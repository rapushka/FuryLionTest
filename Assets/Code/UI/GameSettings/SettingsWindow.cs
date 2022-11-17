using Code.DataStoring.Preferences;
using Code.UI.Windows.Service;
using UnityEngine;

namespace Code.UI.GameSettings
{
	public class SettingsWindow : UnityWindow
	{
		[SerializeField] private SoundSettings _soundSettings;
		
		private Settings _settings;

		public void Construct(Settings settings) => _settings = settings;

		private void OnDisable()
		{
			var settings = new SettingsModel
			{
				PlayingMusic = _soundSettings.MusicIsOn,
				PlayingSFX = _soundSettings.SfxIsOn,
				Locale = _settings.CurrentLocale
			};
			_settings.Storage.Save(settings);
		}
	}
}