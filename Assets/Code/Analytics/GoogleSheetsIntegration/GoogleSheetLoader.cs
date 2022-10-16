using System;
using System.Collections.Generic;

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

		public void DownloadTable() => _googleSheetToCvsDownloader.DownloadTable(OnRawCvsLoaded);

		private void OnRawCvsLoaded(string rawCvsText)
		{
			_handlers = rawCvsText.ProcessData();
			DataProcessed?.Invoke(_handlers);
		}
	}
}