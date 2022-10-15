using Code.Analytics.GoogleSheetsIntegration;
using UnityEditor;

namespace Code.Analytics
{
	public static class AnalyticEventHandlersGenerator
	{
		[MenuItem("Tools/Analytics/Generate handlers")]
		public static void Generate()
		{
			var sheetLoader = Initialize();

			sheetLoader.DownloadTable();
		}

		private static GoogleSheetLoader Initialize()
		{
			var cvsLoader = new CvsLoader();
			var sheetProcessor = new SheetProcessor();
			var sheetLoader = new GoogleSheetLoader(cvsLoader, sheetProcessor);

			InitializeDownloadedTableHandler(sheetLoader);

			return sheetLoader;
		}

		private static void InitializeDownloadedTableHandler(GoogleSheetLoader sheetLoader)
		{
			var cvsLoadedDebug = new CvsLoadedDebug();
			sheetLoader.DataProcessed += cvsLoadedDebug.OnDataProcessed;
		}
	}
}