using System;
using System.IO;
using Code.Analytics.GoogleSheetsIntegration.CvsLoader;

namespace Code.Analytics.GoogleSheetsIntegration.ForDebug
{
	public class LocalCsvLoader : ICsvLoader
	{
		private static string Path => $@"{Directory.GetCurrentDirectory()}\Temp\Sheet.csv";

		public void LoadTable(Action<string> onSheetLoaded)
			=> onSheetLoaded.Invoke(File.ReadAllText(Path));
	}
}