using System.Collections.Generic;
using UnityEngine;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class CvsLoadedDebug
	{
		// googleSheetLoader.DataProcessed += OnDataProcessed
		public void OnDataProcessed(List<AnalyticEventHandler> handlers)
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