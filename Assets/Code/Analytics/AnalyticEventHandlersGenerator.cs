using Code.Analytics.GoogleSheetsIntegration;
using Code.Analytics.HandlersGeneration;
using UnityEditor;
using UnityEngine;

namespace Code.Analytics
{
	public static class AnalyticEventHandlersGenerator
	{
		[MenuItem("Tools/Analytics/Generate handlers")]
		public static void Generate()
		{
			var sheetLoader = Initialize();
			sheetLoader.DownloadTable();

			Debug.Log("Generated");
		}

		private static GoogleSheetLoader Initialize()
		{
			const string sheetId = "1A9Zk0BHFY8-hhSt-A_IZs2s7Z9pjylu4GNhd65EcFMk";
			
			var cvsLoader = new CvsLoader(sheetId);
			var sheetProcessor = new SheetProcessor();
			var sheetLoader = new GoogleSheetLoader(cvsLoader, sheetProcessor);

			InitializeDownloadedTableHandler(sheetLoader);

			return sheetLoader;
		}

		private static void InitializeDownloadedTableHandler(GoogleSheetLoader sheetLoader)
		{
			var handlerGenerator = new HandlerGenerator();
			var signalsGenerator = new SignalsGenerator();
			
			sheetLoader.DataProcessed += handlerGenerator.OnDataProcessed;
			sheetLoader.DataProcessed += signalsGenerator.OnDataProcessed;
		}
	}
}