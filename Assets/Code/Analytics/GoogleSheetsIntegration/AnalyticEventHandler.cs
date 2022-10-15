using System.Collections.Generic;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class AnalyticEventHandler
	{
		public string Event;
		public List<(string type, string name)> Parameters;
		public string Action;
	}
}