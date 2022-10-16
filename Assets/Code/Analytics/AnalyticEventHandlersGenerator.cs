using Code.Analytics.GoogleSheetsIntegration;
using Code.Analytics.GoogleSheetsIntegration.CvsLoader;
using Code.Analytics.HandlersGeneration.Handler;
using Code.Analytics.HandlersGeneration.Signals;
using Code.Analytics.HandlersGeneration.SignalsBindingExtensions;
using Code.Inner;
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
			var cvsLoader = new GoogleSheetAsCvsDownloader(Constants.Analytics.GoogleSheetId);
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