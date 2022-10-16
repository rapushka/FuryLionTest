using Code.Analytics.GoogleSheetsIntegration;
using Code.Analytics.HandlersGeneration.Handler;
using Code.Analytics.HandlersGeneration.Signals;
using Code.Analytics.HandlersGeneration.SignalsBindingExtensions;
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
			
			var cvsLoader = new GoogleSheetToCvsDownloader(sheetId);
			var sheetLoader = new GoogleSheetLoader(cvsLoader);

			InitializeDownloadedTableHandler(sheetLoader);

			return sheetLoader;
		}

		private static void InitializeDownloadedTableHandler(GoogleSheetLoader sheetLoader)
		{
			var handlerGenerator = new HandlerGenerator();
			var signalsGenerator = new SignalsGenerator();
			var bindingsGenerator = new SignalsBindingExtensionsGenerator();
			
			sheetLoader.DataProcessed += handlerGenerator.OnDataProcessed;
			sheetLoader.DataProcessed += signalsGenerator.OnDataProcessed;
			sheetLoader.DataProcessed += bindingsGenerator.OnDataProcessed;
		}
	}
}