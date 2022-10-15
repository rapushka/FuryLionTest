using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class CvsLoadMock : IInitializable
	{
		private readonly GoogleSheetLoader _googleSheetLoader;

		[Inject]
		public CvsLoadMock(GoogleSheetLoader googleSheetLoader)
		{
			_googleSheetLoader = googleSheetLoader;
		}

		public void Initialize()
		{
			_googleSheetLoader.DataProcessed += OnDataProcessed;
		}

		private void OnDataProcessed(List<AnalyticEventHandler> handlers)
		{
			foreach (var handler in handlers)
			{
				Debug.Log($"event — {handler.ColumnEvent}");
				Debug.Log("parameters:");
				foreach (var parameter in handler.ColumnParameters)
				{
					Debug.Log($"\t{parameter.type} {parameter.name}");
				}
				Debug.Log($"action — {handler.ColumnAction}");
			}
		}
	}
}