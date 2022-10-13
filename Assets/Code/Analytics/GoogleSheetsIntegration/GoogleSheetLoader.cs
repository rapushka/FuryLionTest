using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class GoogleSheetLoader : MonoBehaviour
	{
		[SerializeField] private string _sheetId;
    
		private CvsLoader _cvsLoader;
		private SheetProcessor _sheetProcessor;
		private List<AnalyticEventHandler> _handlers;

		public event Action<List<AnalyticEventHandler>> OnProcessData;
		
		private void Start()
		{
			_cvsLoader = GetComponent<CvsLoader>();
			_sheetProcessor = GetComponent<SheetProcessor>();
			DownloadTable();
		}

		private void DownloadTable() => _cvsLoader.DownloadTable(_sheetId, OnRawCvsLoaded);

		private void OnRawCvsLoaded(string rawCvsText)
		{
			_handlers = _sheetProcessor.ProcessData(rawCvsText);
			OnProcessData?.Invoke(_handlers);
		}
	}
}