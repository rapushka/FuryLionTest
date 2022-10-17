using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Analytics.GoogleSheetsIntegration.ForDebug
{
	public class LogLoadedCsv
	{
		public void OnDataProcessed(List<AnalyticEventHandler> handlers)
		{
			foreach (var handler in handlers)
			{
				var message = string.Empty;
				
				message += $"event — {handler.Event}\n";

				if (handler.Parameters.Any())
				{
					message += "parameters: ";
				}
				message = handler.Parameters.Aggregate(message, (m, p) => m + $" {p.type} {p.name} ");
				message += '\n';

				message += $"action — {handler.Action}\n";
				Debug.Log(message);
			}
		}
	}
}