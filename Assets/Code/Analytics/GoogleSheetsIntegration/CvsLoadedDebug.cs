using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class CvsLoadedDebug
	{
		public void OnDataProcessed(List<AnalyticEventHandler> handlers)
		{
			foreach (var handler in handlers)
			{
				var message = string.Empty;
				
				message += $"event — {handler.ColumnEvent}\n";

				if (handler.ColumnParameters.Any())
				{
					message += "parameters: ";
				}
				message = handler.ColumnParameters.Aggregate(message, (m, p) => m + $" {p.type} {p.name} ");
				message += '\n';

				message += $"action — {handler.ColumnAction}\n";
				Debug.Log(message);
			}
		}
	}
}