using Code.DataStoring.Preferences;
using Code.Generated.Analytics.Signals;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;
using static Code.Inner.Constants.AudioSettings.ExposedParameter;
using static Code.Inner.Constants.AudioSettings;

namespace Code.UI.GameSettings
{
	public class SoundSettings : MonoBehaviour
	{
		[SerializeField] private Toggle _musicToggle;
		[SerializeField] private Toggle _sfxToggle;

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

			SetMusicVolume(settings.PlayingMusic);
			SetSfxVolume(settings.PlayingSFX);
		}

		private void OnMusicToggle(bool isEnabled)
		{
			var volume = SetMusicVolume(isEnabled);
			_signalBus.Fire(new MusicChangedSignal(CalculatePercentage(volume)));
		}

		private void OnSfxToggle(bool isEnabled)
		{
			var volume = SetSfxVolume(isEnabled);
			_signalBus.Fire(new SoundChangedSignal(CalculatePercentage(volume)));
		}

		private float SetMusicVolume(bool isEnabled) => ToggleAudioMixerParameter(isEnabled, MusicVolume);

		private float SetSfxVolume(bool isEnabled) => ToggleAudioMixerParameter(isEnabled, SfxVolume);

		private float ToggleAudioMixerParameter(bool isEnabled, string nameOfMixer)
		{
			float volume = isEnabled ? MaxAudioVolume : MinAudioVolume;
			_audioMixer.SetFloat(nameOfMixer, volume);
			return volume;
		}

		private static float CalculatePercentage(float volume) 
			=> (volume + NegativeOffsetCompensation) / WholeValue * 100;
	}
}