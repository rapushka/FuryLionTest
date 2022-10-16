using System;
using System.Collections.Generic;
using Code.Analytics.GoogleSheetsIntegration.CvsLoader;
using Code.Extensions.GoogleSheetsParsing;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class GoogleSheetLoader
	{
		private readonly GoogleSheetToCvsDownloader _googleSheetToCvsDownloader;
		
		private List<AnalyticEventHandler> _handlers;

		public event Action<List<AnalyticEventHandler>> DataProcessed;

		public GoogleSheetLoader(GoogleSheetToCvsDownloader googleSheetToCvsDownloader)
		{
			_googleSheetToCvsDownloader = googleSheetToCvsDownloader;
		}

		public void DownloadTable() => _googleSheetToCvsDownloader.LoadTable(OnRawCvsLoaded);

		private void OnRawCvsLoaded(string rawCvsText)
		{
			_handlers = rawCvsText.ProcessData();
			DataProcessed?.Invoke(_handlers);
		}
	}
}