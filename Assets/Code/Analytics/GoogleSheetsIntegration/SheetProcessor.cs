using System.Collections.Generic;
using System.Linq;
using Code.Extensions.GoogleSheetsParsing;
using Zenject;

namespace Code.Analytics.GoogleSheetsIntegration
{
	public class SheetProcessor
	{
		private const char Separator = ',';

		[Inject] public SheetProcessor() { }

		public List<AnalyticEventHandler> ProcessData(string cvsRawData)
		{
			const int dataStartRawIndex = 1;

			var lineEnding = GetPlatformSpecificLineEnd();
			var rows = cvsRawData.Split(lineEnding);

			var list = new List<AnalyticEventHandler>();

			for (var i = dataStartRawIndex; i < rows.Length; i++)
			{
				var cells = rows[i].Split(Separator);

				var columnEvent = cells.First().AsMethodName();
				var columnParameters = cells.GetParsedParameters();
				var columnAction = cells.Last();

				list.Add
				(
					new AnalyticEventHandler
					{
						ColumnEvent = columnEvent,
						ColumnParameters = columnParameters,
						ColumnAction = columnAction
					}
				);
			}

			return list;
		}

		private char GetPlatformSpecificLineEnd()
		{
			var lineEnding = '\n';
#if UNITY_IOS
			lineEnding = '\r';
#endif
			return lineEnding;
		}
	}
}