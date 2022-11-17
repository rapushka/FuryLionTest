using Code.DataStoring;
using Code.DataStoring.Preferences;
using Code.UI.Windows.Service;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.GameSettings
{
	public class SettingsWindow : UnityWindow
	{
		[SerializeField] private Button _buttonOK;
		[SerializeField] private SoundSettings _soundSettings;

		private IStorage _storage;
		private LanguageSelector _languageSelector;

		public void Construct(IStorage storage, LanguageSelector language)
		{
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

			var settings = new SettingsModel
			{
				PlayingMusic = _soundSettings.MusicIsOn,
				PlayingSFX = _soundSettings.SfxIsOn,
				Locale = _languageSelector.CurrentLocale
			};
			_storage.Save(settings);
		}

		public void LoadSettings()
		{
			var settings = _storage.Load(SettingsModel.DefaultSettingsModel);
			_soundSettings.LoadSettings(settings);
			_languageSelector.CurrentLocale = settings.Locale;
		}

		private void CloseWindow() => gameObject.SetActive(false);
	}
}