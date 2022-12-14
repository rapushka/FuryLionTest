using Code.Analytics.GoogleSheetsIntegration;
using Code.Analytics.GoogleSheetsIntegration.CvsLoader;
using Code.Analytics.HandlersGeneration.Handler;
using Code.Analytics.HandlersGeneration.Signals;
using Code.Analytics.HandlersGeneration.SignalsBindingExtensions;
using UnityEditor;
using UnityEngine;
using static Code.Inner.Constants.Analytics;

namespace Code.Inner.Editor
{
	public static class AnalyticGeneratorMenuItem
	{
		[MenuItem("Tools/Analytics/Generate handlers")]
		public static void Generate()
		{
			var handlersLoader = InitializeSheetLoader();
			SubscribeGenerators(handlersLoader);
			handlersLoader.DownloadTable();

			Debug.Log("Generated");
		}

		private static HandlersLoader InitializeSheetLoader() => new(new GoogleSheetAsCsvDownloader(GoogleSheetId));

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