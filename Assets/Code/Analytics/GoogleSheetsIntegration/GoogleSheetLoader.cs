using System;
using System.Collections.Generic;
using Code.Analytics.GoogleSheetsIntegration.CvsLoader;
using Code.Extensions.GoogleSheetsParsing;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class GoogleSheetLoader
	{
		private readonly ICsvLoader _csvLoader;
		
		private List<AnalyticEventHandler> _handlers;

		public event Action<List<AnalyticEventHandler>> DataProcessed;

		public GoogleSheetLoader(ICsvLoader csvLoader) => _csvLoader = csvLoader;

		public void DownloadTable() => _csvLoader.LoadTable(OnRawCvsLoaded);

		private void OnRawCvsLoaded(string rawCvsText)
		{
			_handlers = rawCvsText.ProcessData();
			DataProcessed?.Invoke(_handlers);
		}
	}
}