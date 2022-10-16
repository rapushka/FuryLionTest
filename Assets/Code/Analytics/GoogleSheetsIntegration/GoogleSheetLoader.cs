using System;
using System.Collections.Generic;
using Code.Analytics.GoogleSheetsIntegration.CvsLoader;
using Code.Extensions.GoogleSheetsParsing;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class GoogleSheetLoader
	{
		private readonly ICvsLoader _cvsLoader;
		
		private List<AnalyticEventHandler> _handlers;

		public event Action<List<AnalyticEventHandler>> DataProcessed;

		public GoogleSheetLoader(ICvsLoader cvsLoader) => _cvsLoader = cvsLoader;

		public void DownloadTable() => _cvsLoader.LoadTable(OnRawCvsLoaded);

		private void OnRawCvsLoaded(string rawCvsText)
		{
			_handlers = rawCvsText.ProcessData();
			DataProcessed?.Invoke(_handlers);
		}
	}
}