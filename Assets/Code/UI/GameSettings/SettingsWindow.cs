using Code.DataStoring;
using Code.DataStoring.Preferences;
using Code.Generated.Analytics.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.GameSettings
{
	public class SettingsWindow : MonoBehaviour
	{
		[SerializeField] private Button _buttonOK;
		[SerializeField] private SoundSettings _soundSettings;

		private IStorage _storage;
		private LanguageSelector _languageSelector;
		private SignalBus _signalBus;

		[Inject]
		public void Construct(IStorage storage, LanguageSelector language, SignalBus signalBus)
		{
			_signalBus = signalBus;
			_languageSelector = language;
			_storage = storage;
		}

		private void OnEnable()
		{
			_buttonOK.onClick.AddListener(CloseWindow);

			LoadSettings();
		}

		private void OnDisable()
		{
			_buttonOK.onClick.RemoveListener(CloseWindow);

			var settings = new Settings
			{
				PlayingMusic = _soundSettings.MusicIsOn,
				PlayingSFX = _soundSettings.SfxIsOn,
				Locale = _languageSelector.CurrentLocale
			};
			_storage.Save(settings);
		}

		public void LoadSettings()
		{
			var settings = _storage.Load(Settings.DefaultSettings);
			_soundSettings.LoadSettings(settings);
			_languageSelector.CurrentLocale = settings.Locale;
		}

		public void OpenWindow()
		{
			if (gameObject.activeSelf)
			{
				return;
			}

			_signalBus.Fire<SettingsOpenedSignal>();
			gameObject.SetActive(true);
		}

		private void CloseWindow() => gameObject.SetActive(false);
	}
}