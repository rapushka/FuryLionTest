using System;
using Code.DataStoring;
using Code.DataStoring.Preferences;
using Code.Generated.Analytics.Signals;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.GameSettings
{
	public class SettingsWindow : MonoBehaviour
	{
		[SerializeField] private Button _buttonOK;
		[SerializeField] private Toggle _musicToggle;
		[SerializeField] private Toggle _sfxToggle;

		private const string MusicVolume = nameof(MusicVolume);
		private const string SfxVolume = nameof(SfxVolume);

		private AudioMixer _audioMixer;
		private IStorage _storage;
		private LanguageSelector _languageSelector;
		private SignalBus _signalBus;

		[Inject]
		public void Construct(AudioMixer audioMixer, IStorage storage, LanguageSelector language, SignalBus signalBus)
		{
			_signalBus = signalBus;
			_languageSelector = language;
			_audioMixer = audioMixer;
			_storage = storage;
		}

		private void OnEnable() => LoadSettings();

		private void OnDisable()
		{
			_buttonOK.onClick.RemoveListener(CloseWindow);
			_musicToggle.onValueChanged.RemoveListener(OnMusicToggle);
			_sfxToggle.onValueChanged.RemoveListener(OnSfxToggle);

			var settings = new Settings
			{
				PlayingMusic = _musicToggle.isOn,
				PlayingSFX = _sfxToggle.isOn,
				Locale = _languageSelector.CurrentLocale
			};
			_storage.Save(settings);
		}

		public void LoadSettings()
		{
			_buttonOK.onClick.AddListener(CloseWindow);
			_musicToggle.onValueChanged.AddListener(OnMusicToggle);
			_sfxToggle.onValueChanged.AddListener(OnSfxToggle);

			var settings = _storage.Load(Settings.DefaultSettings);
			_musicToggle.isOn = settings.PlayingMusic;
			_sfxToggle.isOn = settings.PlayingSFX;
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

		private void OnMusicToggle(bool isEnabled)
			=> ToggleAudio(isEnabled, MusicVolume, (v) => new MusicChangedSignal(v));

		private void OnSfxToggle(bool isEnabled)
			=> ToggleAudio(isEnabled, SfxVolume, (v) => new SoundChangedSignal(v));

		private void ToggleAudio(bool isEnabled, string nameOfMixer, Func<float, object> newSignal)
		{
			var volume = ToggleAudioMixerParameter(isEnabled, nameOfMixer);
			_signalBus.Fire(newSignal.Invoke(CalculatePercentage(volume)));
		}

		private float ToggleAudioMixerParameter(bool isEnabled, string nameOfMixer)
		{
			const int maxSoundVolume = 0;
			const int minSoundVolume = -80;

			float volume = isEnabled ? maxSoundVolume : minSoundVolume;
			_audioMixer.SetFloat(nameOfMixer, volume);
			return volume;
		}

		private static float CalculatePercentage(float volume)
		{
			const int negativeOffsetCompensation = 80;
			const int wholeValue = 80;

			return (volume + negativeOffsetCompensation) / wholeValue * 100;
		}
	}
}