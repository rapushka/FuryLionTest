using System;
using System.Collections;
using System.Linq;
using Code.DataStoring.Localizations;
using Code.Inner.CustomMonoBehaviours;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Zenject;

namespace Code.UI.GameSettings
{
	public class LanguageSelector : MonoBehaviour
	{
		[SerializeField] private TMP_Dropdown _dropdown;

		private bool _isActive;
		private int _currentLocaleId;
		private CoroutinesHandler _coroutines;

		[Inject]
		public void Construct(CoroutinesHandler coroutines)
		{
			_coroutines = coroutines;
		}
		
		public LanguageLocale CurrentLocale
		{
			get => (LanguageLocale)_currentLocaleId;
			set => OnValueChanged((int)value);
		}

		private void OnEnable()
		{
			_dropdown.onValueChanged.AddListener(OnValueChanged);
			_dropdown.AddOptions(Enum.GetNames(typeof(LanguageLocale)).ToList());
		}

		private void OnDisable()
		{
			_dropdown.onValueChanged.RemoveListener(OnValueChanged);
			_dropdown.ClearOptions();
		}

		private void OnValueChanged(int selectedIndex)
		{
			if (_isActive == false)
			{
				_coroutines.StartRoutine(SetLocale(selectedIndex));
			} 
		}

		private IEnumerator SetLocale(int localeId)
		{
			_isActive = true;

			_currentLocaleId = localeId;
			yield return LocalizationSettings.InitializationOperation;
			LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
			
			_isActive = false;
		} 
	}
}