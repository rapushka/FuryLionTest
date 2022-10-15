using System.Collections.Generic;
using Code.Extensions.Generation;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class AnalyticEventHandler
	{
		public string Event;
		public List<(string type, string name)> Parameters;
		public string Action;

		public (string action, string @event, string methodParams, string invokeParams) Deconstruct() 
			=> (Action, Event, Parameters.GetMethodParameters(), Parameters.GetInvokeParameters());
	}
}