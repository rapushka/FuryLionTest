using System.Collections.Generic;
using System.IO;
using System.Text;
using Code.Analytics.GoogleSheetsIntegration;
using Code.Extensions.Generation;
using Code.Extensions.GoogleSheetsParsing;

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

			var codeTemplate = @$"// Generated
using Code.Analytics;

namespace {@namespace}
{{
	public class {className}
	{{
		private readonly AnalyticsCollection _analytics = new();

{GenerateCodeForHandlers()}
	}}
}}
";
			streamWriter.Write(codeTemplate);
		}

		private string GenerateCodeForHandlers()
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
			=> $@"		// Action: {handler.Action}
		public void On{handler.Event}({handler.Parameters.GetMethodParameters()})
		{{
			_analytics.HandleEvent(""{handler.Event}""{handler.Parameters.GetInvokeParameters()});
		}}
";
	}
}