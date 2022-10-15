using System.Collections.Generic;
using System.Linq;
using Code.Extensions.GoogleSheetsParsing;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class SheetProcessor
	{
		private const char Separator = ',';

		public List<AnalyticEventHandler> ProcessData(string cvsRawData)
		{
			const int indexOfFirstRowWithData = 1;

			var rows = cvsRawData.Split("\n\r");

			var list = new List<AnalyticEventHandler>();

			for (var i = indexOfFirstRowWithData; i < rows.Length; i++)
			{
				var cells = rows[i].Split(Separator);

				var columnEvent = cells.First().AsMethodName();
				var columnParameters = cells.GetParsedParameters();
				var columnAction = cells.Last().Replace("\n\r", "\0");

				list.Add
				(
					new AnalyticEventHandler
					{
						Event = columnEvent,
						Parameters = columnParameters,
						Action = columnAction
					}
				);
			}

			return list;
		}
	}
}