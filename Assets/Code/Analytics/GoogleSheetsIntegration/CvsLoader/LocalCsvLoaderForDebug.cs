using System;
using System.IO;

namespace Code.Analytics.GoogleSheetsIntegration.CvsLoader
{
	public class LocalCsvLoaderForDebug : ICsvLoader
	{
		private static string Path => $@"{Directory.GetCurrentDirectory()}\Temp\Sheet.csv";

		public void LoadTable(Action<string> onSheetLoaded)
			=> onSheetLoaded.Invoke(File.ReadAllText(Path));
	}
}