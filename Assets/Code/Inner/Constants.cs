namespace Code.Inner
{
	public static class Constants
	{
		public static class GameFieldSize
		{
			public const int Width = 7;
			public const int Height = 11;
		}

		public static class SceneIndex
		{
			public const int Gameplay = 0;
			public const int Lose = 1;
			public const int Victory = 2;
		}

		public static class Analytics
		{
			public const string GoogleSheetId = "1A9Zk0BHFY8-hhSt-A_IZs2s7Z9pjylu4GNhd65EcFMk";
			public const string SheetExportAsCsvUrl = "https://docs.google.com/spreadsheets/d/*/export?format=csv";
		}

		public static class AudioSettings
		{
			public const int MaxAudioVolume = 0;
			public const int MinAudioVolume = -NegativeOffsetCompensation;
			
			public const int NegativeOffsetCompensation = 80;
			public const int WholeValue = 80;

			public static class ExposedParameter
			{
				public const string MusicVolume = nameof(MusicVolume);
				public const string SfxVolume = nameof(SfxVolume);
			}
		}
	}
}