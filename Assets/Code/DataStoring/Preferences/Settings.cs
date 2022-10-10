using System;
using Code.DataStoring.Localizations;

namespace Code.DataStoring.Preferences
{
	[Serializable]
	public class Settings
	{
		public bool PlayingMusic;
		public bool PlayingSFX;
		public LanguageLocale Locale;

		public static Settings DefaultSettings => new()
		{
			PlayingMusic = true,
			PlayingSFX = true,
			Locale = LanguageLocale.English
		};
	}
}