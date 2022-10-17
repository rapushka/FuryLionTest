using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.Analytics.GoogleSheetsIntegration;

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

			var code = GenerateClass(@namespace, className);
			file.Write(code);
		}

		private string GenerateClass(string @namespace, string className)
			=> HandlerCodeTemplates.Class(@namespace, className, GenerateMethods());

		private string GenerateMethods() => string.Join('\n', HandlersAsTemplates());

		private IEnumerable<string> HandlersAsTemplates() => _handlers.Select(HandlerCodeTemplates.Method);
	}
}