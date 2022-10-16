using System.Collections.Generic;
using System.IO;
using System.Text;
using Code.Analytics.GoogleSheetsIntegration;
using Code.Extensions.Generation;

namespace Code.Analytics.HandlersGeneration.Handler
{
	public class HandlerGenerator
	{
		private List<AnalyticEventHandler> _handlers;

		public void OnDataProcessed(List<AnalyticEventHandler> handlers)
		{
			_handlers = handlers;

			const string @namespace = "Code.Generated.Analytics";
			const string className = "AnalyticEventsHandler";

			var path = $@"{Directory.GetCurrentDirectory()}\Assets\{@namespace.Replace('.', '\\')}";
			using var file = File.CreateText(path + @$"\{className}.cs");

			file.Write(GenerateClass(@namespace, className));
		}

		private string GenerateClass(string @namespace, string className)
			=> AnalyticEventHandlerCodeTemplates.Class(@namespace, className, GenerateMethods());

		private string GenerateMethods()
		{
			const int twoLineBreaks = 4;

			var result = new StringBuilder();

			foreach (var handler in _handlers)
			{
				result.AppendLine(GenerateHandler(handler));
			}

			result.RemoveLasSymbols(twoLineBreaks);

			return result.ToString();
		}

		private static string GenerateHandler(AnalyticEventHandler handler)
			=> AnalyticEventHandlerCodeTemplates.Method(handler.Deconstruct());
	}
}