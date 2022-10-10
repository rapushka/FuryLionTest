using System;
using System.Collections;
using System.Linq;
using Code.DataStoring.Localizations;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Code.UI.GameSettings
{
	public class LanguageSelector : MonoBehaviour
	{
		[SerializeField] private TMP_Dropdown _dropdown;

		private bool _isActive;
		private int _currentLocaleId;

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

		private void OnValueChanged(int selectedIndex)
		{
			if (_isActive == false)
			{
				StartCoroutine(SetLocale(selectedIndex));
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