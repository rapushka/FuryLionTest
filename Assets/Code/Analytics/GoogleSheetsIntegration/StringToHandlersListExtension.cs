using System.Collections.Generic;
using System.Linq;
using Code.Extensions.GoogleSheetsParsing;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public static class StringToHandlersListExtension
	{
		private const string LineBreak = "\r\n";
		private const int HeaderRowsCount = 1;
		private const char Separator = ',';

		public static List<AnalyticEventHandler> ProcessData(this string cvsRawData)
			=> cvsRawData
			   .Split(LineBreak)
			   .Skip(HeaderRowsCount)
			   .Select((row) => row.Split(Separator).ParseToHandler())
			   .ToList();

		private static AnalyticEventHandler ParseToHandler(this string[] cells)
			=> new()
			{
				Event = cells.First().AsMethodName(),
				Parameters = cells.GetParsedParameters(),
				Action = cells.Last()
			};
	}
}