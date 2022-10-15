using System;
using System.Collections.Generic;
using Zenject;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class GoogleSheetLoader : IInitializable
	{
		private const string SheetId = "1A9Zk0BHFY8-hhSt-A_IZs2s7Z9pjylu4GNhd65EcFMk";

		private readonly CvsLoader _cvsLoader;
		private readonly SheetProcessor _sheetProcessor;
		
		private List<AnalyticEventHandler> _handlers;

		public event Action<List<AnalyticEventHandler>> DataProcessed;

		[Inject]
		public GoogleSheetLoader(CvsLoader cvsLoader, SheetProcessor sheetProcessor)
		{
			_cvsLoader = cvsLoader;
			_sheetProcessor = sheetProcessor;
		}

		public void Initialize() => DownloadTable();

		private void DownloadTable() => _cvsLoader.DownloadTable(SheetId, OnRawCvsLoaded);

		private void OnRawCvsLoaded(string rawCvsText)
		{
			_handlers = _sheetProcessor.ProcessData(rawCvsText);
			DataProcessed?.Invoke(_handlers);
		}
	}
}