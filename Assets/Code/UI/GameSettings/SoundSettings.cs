using System;
using Code.DataStoring.Preferences;
using Code.Generated.Analytics.Signals;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.GameSettings
{
	public class SoundSettings : MonoBehaviour
	{
		[SerializeField] private Toggle _musicToggle;
		[SerializeField] private Toggle _sfxToggle;

		private const int MaxAudioVolume = 0;
		private const int MinAudioVolume = -80;

		private const string MusicVolume = nameof(MusicVolume);
		private const string SfxVolume = nameof(SfxVolume);

		private AudioMixer _audioMixer;
		private SignalBus _signalBus;

		public bool MusicIsOn => _musicToggle.isOn;

		public bool SfxIsOn => _sfxToggle.isOn;

		[Inject]
		public void Construct(AudioMixer audioMixer, SignalBus signalBus)
		{
			_audioMixer = audioMixer;
			_signalBus = signalBus;
		}

		private void OnEnable()
		{
			_musicToggle.onValueChanged.AddListener(OnMusicToggle);
			_sfxToggle.onValueChanged.AddListener(OnSfxToggle);
		}

		private void OnDisable()
		{
			_musicToggle.onValueChanged.RemoveListener(OnMusicToggle);
			_sfxToggle.onValueChanged.RemoveListener(OnSfxToggle);
		}

		public void LoadSettings(Settings settings)
		{
			_musicToggle.isOn = settings.PlayingMusic;
			_sfxToggle.isOn = settings.PlayingSFX;
		}

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
			float volume = isEnabled ? MaxAudioVolume : MinAudioVolume;
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