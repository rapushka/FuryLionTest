using System.Collections.Generic;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class AnalyticEventHandler
	{
		public string ColumnEvent;
		public List<(string type, string name)> ColumnParameters;
		public string ColumnAction;
	}
}