using Code.DataStoring.Preferences;
using Code.Generated.Analytics.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static Code.Inner.Constants.AudioSettings;

namespace Code.UI.GameSettings
{
	public class SoundSettings : MonoBehaviour
	{
		[SerializeField] private Toggle _musicToggle;
		[SerializeField] private Toggle _sfxToggle;

		private SignalBus _signalBus;
		private Settings _settings;

		public bool MusicIsOn => _musicToggle.isOn;

		public bool SfxIsOn => _sfxToggle.isOn;

		[Inject]
		public void Construct(Settings settings, SignalBus signalBus)
		{
			_settings = settings;
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

		public void LoadSettings(SettingsModel settingsModel)
		{
			_musicToggle.isOn = settingsModel.PlayingMusic;
			_sfxToggle.isOn = settingsModel.PlayingSFX;
		}

		private void OnMusicToggle(bool isEnabled)
		{
			var volume = _settings.SetMusicVolume(isEnabled);
			_signalBus.Fire(new MusicChangedSignal(CalculatePercentage(volume)));
		}

		private void OnSfxToggle(bool isEnabled)
		{
			var volume = _settings.SetSfxVolume(isEnabled);
			_signalBus.Fire(new SoundChangedSignal(CalculatePercentage(volume)));
		}

		private static float CalculatePercentage(float volume)
			=> (volume + NegativeOffsetCompensation) / WholeValue * 100;
	}
}