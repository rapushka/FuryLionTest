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
			var handlersLoader = InitializeSheetLoader();
			SubscribeGenerators(handlersLoader);
			handlersLoader.DownloadTable();

			Debug.Log("Generated");
		}

		private static HandlersLoader InitializeSheetLoader()
		{
			// var csvLoader = new GoogleSheetAsCsvDownloader(Constants.Analytics.GoogleSheetId);
			var csvLoader = new LocalCsvLoaderForDebug();

			return new HandlersLoader(csvLoader);
		}

		private static void SubscribeGenerators(HandlersLoader handlersLoader)
		{
			var handlerGenerator = new HandlerGenerator();
			var signalsGenerator = new SignalsGenerator();
			var bindingsGenerator = new SignalsBindingExtensionsGenerator();
			
			handlersLoader.DataProcessed += handlerGenerator.OnDataProcessed;
			handlersLoader.DataProcessed += signalsGenerator.OnDataProcessed;
			handlersLoader.DataProcessed += bindingsGenerator.OnDataProcessed;
		}
	}
}