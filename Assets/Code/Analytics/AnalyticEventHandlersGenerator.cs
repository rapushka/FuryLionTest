using Code.Analytics.GoogleSheetsIntegration;
using Code.Analytics.GoogleSheetsIntegration.CvsLoader;
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
			var sheetLoader = InitializeSheetLoader();
			sheetLoader.DownloadTable();

			Debug.Log("Generated");
		}

		private static GoogleSheetLoader InitializeSheetLoader()
		{
			var debugCsvLoader = new LocalCsvLoaderForDebug();
			var sheetLoader = new GoogleSheetLoader(debugCsvLoader);

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