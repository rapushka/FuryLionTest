using System.Collections.Generic;
using System.Linq;
using Code.Extensions.GoogleSheetsParsing;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public static class StringToHandlersListExtension
	{
		private const char Separator = ',';
		private const int HeaderRowsCount = 1;
		private const string LineBreak = "\r\n";

		public static List<AnalyticEventHandler> ProcessData(this string cvsRawData)
			=> cvsRawData
			   .Split(LineBreak)
			   .Skip(HeaderRowsCount)
			   .Select((row) => row.Split(Separator))
			   .Select((cells) => cells.ParseToHandler())
			   .ToList();

		private static AnalyticEventHandler ParseToHandler(this string[] cells)
		{
			var columnEvent = cells.First().AsMethodName();
			var columnParameters = cells.GetParsedParameters();
			var columnAction = cells.Last();

			var newHandler = new AnalyticEventHandler
			{
				Event = columnEvent,
				Parameters = columnParameters,
				Action = columnAction
			};
			return newHandler;
		}
	}
}