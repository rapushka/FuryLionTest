using System.Collections.Generic;
using System.IO;
using System.Text;
using Code.Analytics.GoogleSheetsIntegration;
using Code.Extensions.Generation;

namespace Code.Analytics.HandlersGeneration
{
	public class Generator
	{
		private List<AnalyticEventHandler> _handlers;

		public void OnDataProcessed(List<AnalyticEventHandler> handlers)
		{
			_handlers = handlers;
			
			const string @namespace = "Code.Generated.Analytics";
			const string className = "AnalyticEventsHandler";

			var path = $@"{Directory.GetCurrentDirectory()}\Assets\{@namespace.Replace('.', '\\')}";
			using var streamWriter = File.CreateText(path + @$"\{className}.cs");

			var code = CodeTemplates.AnalyticEventHandlerClass(@namespace, className, GenerateHandlers());
			
			streamWriter.Write(code);
		}

		private string GenerateHandlers()
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
			=> CodeTemplates.AnalyticEventHandlerMethod
			(
				handler.Action,
				handler.Event,
				handler.Parameters.GetMethodParameters(),
				handler.Parameters.GetInvokeParameters()
			);
	}
}