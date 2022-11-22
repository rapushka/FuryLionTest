using Code.DataStoring;
using Code.DataStoring.Localizations;
using Code.DataStoring.Preferences;
using Code.UI.Windows.Panels;
using UnityEngine.Audio;
using Zenject;
using static Code.Inner.Constants.AudioSettings.ExposedParameter;
using static Code.Inner.Constants.AudioSettings;

namespace Code.UI.GameSettings
{
	public class Settings : IInitializable
	{
		private readonly SettingsWindow _settingsWindow;
		private readonly LanguageSelector _languageSelector;
		private readonly AudioMixer _audioMixer;

		[Inject]
		public Settings(IStorage storage, LanguageSelector language, AudioMixer audioMixer)
		{
			_languageSelector = language;
			Storage = storage;
			_audioMixer = audioMixer;
		}

		public LanguageLocale CurrentLocale
		{
			get => _languageSelector.CurrentLocale;
			set => _languageSelector.CurrentLocale = value;
		}

		public IStorage Storage { get; }

		public void Initialize() => LoadSettings();

		public float SetMusicVolume(bool isEnabled) => ToggleAudioMixerParameter(isEnabled, MusicVolume);

		public float SetSfxVolume(bool isEnabled) => ToggleAudioMixerParameter(isEnabled, SfxVolume);

		private void LoadSettings()
		{
			var settings = Storage.Load(SettingsModel.DefaultSettingsModel);

			SetMusicVolume(settings.PlayingMusic);
			SetSfxVolume(settings.PlayingSFX);

			_languageSelector.CurrentLocale = settings.Locale;
		}

		private float ToggleAudioMixerParameter(bool isEnabled, string nameOfMixer)
		{
			float volume = isEnabled ? MaxAudioVolume : MinAudioVolume;
			_audioMixer.SetFloat(nameOfMixer, volume);
			return volume;
		}
	}
}