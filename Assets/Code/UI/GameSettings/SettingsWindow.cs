using Code.DataStoring;
using Code.DataStoring.Preferences;
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

		[Inject]
		public void Construct(AudioMixer audioMixer, IStorage storage)
		{
			_audioMixer = audioMixer;
			_storage = storage;
		}

		private void OnEnable()
		{
			_buttonOK.onClick.AddListener(CloseWindow);
			_musicToggle.onValueChanged.AddListener(OnMusicToggle);
			_sfxToggle.onValueChanged.AddListener(OnSfxToggle);

			var settings = _storage.Load(Settings.DefaultSettings);
			_musicToggle.isOn = settings.PlayingMusic;
			_sfxToggle.isOn = settings.PlayingSFX;
		}

		private void OnDisable()
		{
			_buttonOK.onClick.RemoveListener(CloseWindow);
			_musicToggle.onValueChanged.RemoveListener(OnMusicToggle);
			_sfxToggle.onValueChanged.RemoveListener(OnSfxToggle);
			
			var settings = new Settings
			{
				PlayingMusic = _musicToggle.isOn,
				PlayingSFX = _sfxToggle.isOn,
			};
			_storage.Save(settings);
		}

		public void OpenWindow() => gameObject.SetActive(true);

		private void CloseWindow() => gameObject.SetActive(false);
		
		private void OnMusicToggle(bool isEnabled) => ToggleAudioMixerParameter(isEnabled, MusicVolume);

		private void OnSfxToggle(bool isEnabled) => ToggleAudioMixerParameter(isEnabled, SfxVolume);

		private void ToggleAudioMixerParameter(bool isEnabled, string sfxVolume)
		{
			const int defaultSoundVolume = 0;
			const int soundTurnedOff = -80;

			float volume = isEnabled ? defaultSoundVolume : soundTurnedOff;
			_audioMixer.SetFloat(sfxVolume, volume);
		}
	}
}