using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Settings
{
	public class SettingsWindow : MonoBehaviour
	{
		[SerializeField] private Button _buttonOK;
		[SerializeField] private Toggle _musicToggle;
		[SerializeField] private Toggle _sfxToggle;

		private const string MusicVolume = nameof(MusicVolume);
		private const string SfxVolume = nameof(SfxVolume);
		private AudioMixer _audioMixer;

		[Inject]
		public void Construct(AudioMixer audioMixer)
		{
			_audioMixer = audioMixer;
		}

		private void OnEnable()
		{
			_buttonOK.onClick.AddListener(CloseWindow);
			_musicToggle.onValueChanged.AddListener(OnMusicToggle);
			_sfxToggle.onValueChanged.AddListener(OnSfxToggle);
		}

		private void OnDisable()
		{
			_buttonOK.onClick.RemoveListener(CloseWindow);
			_musicToggle.onValueChanged.RemoveListener(OnMusicToggle);
			_sfxToggle.onValueChanged.RemoveListener(OnSfxToggle);
		}

		public void OpenWindow() => gameObject.SetActive(true);

		private void OnMusicToggle(bool isEnabled) => ToggleAudioMixerParameter(isEnabled, MusicVolume);

		private void OnSfxToggle(bool isEnabled) => ToggleAudioMixerParameter(isEnabled, SfxVolume);

		private void ToggleAudioMixerParameter(bool isEnabled, string sfxVolume)
		{
			const int defaultSoundVolume = 0;
			const int soundTurnedOff = -80;

			float volume = isEnabled ? defaultSoundVolume : soundTurnedOff;
			_audioMixer.SetFloat(sfxVolume, volume);
		}

		private void CloseWindow() => gameObject.SetActive(false);
	}
}