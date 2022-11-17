using System;
using Code.DataStoring.Localizations;

namespace Code.DataStoring.Preferences
{
	[Serializable]
	public class SettingsModel
	{
		public bool PlayingMusic;
		public bool PlayingSFX;
		public LanguageLocale Locale;

		public static SettingsModel DefaultSettingsModel => new()
		{
			PlayingMusic = true,
			PlayingSFX = true,
			Locale = LanguageLocale.English
		};
	}
}