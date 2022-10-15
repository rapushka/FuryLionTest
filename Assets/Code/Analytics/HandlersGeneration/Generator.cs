using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Code.Analytics.GoogleSheetsIntegration;

namespace Code.Analytics.HandlersGeneration
{
	public class Generator
	{
		public void OnDataProcessed(List<AnalyticEventHandler> handlers)
		{
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

{GenerateCodeForHandlers(handlers)}
	}}
}}
";
			streamWriter.Write(codeTemplate);
		}

		private string GenerateCodeForHandlers(List<AnalyticEventHandler> handlers)
		{
			var result = new StringBuilder();

			foreach (var handler in handlers)
			{
				result.AppendLine(GenerateHandler(handler));
			}

			return result.ToString();
		}

		private string GenerateHandler(AnalyticEventHandler handler)
			=> $@"		// Action: {handler.Action}
		public void {handler.Event}({GetMethodParameters(handler.Parameters)})
		{{
			_analytics.HandleEvent(""{handler.Event}""{GetNamedParameters(handler.Parameters)});
		}}
";

		private string GetMethodParameters(List<(string type, string name)> parameters)
		{
			if (parameters.Any() == false)
			{
				return string.Empty;
			}

			var stringBuilder = new StringBuilder();
			foreach (var parameter in parameters)
			{
				stringBuilder.Append($"{parameter.type} {parameter.name}, ");
			}

			stringBuilder.Remove(stringBuilder.Length - 2, 2);
			return stringBuilder.ToString();
		}

		private string GetNamedParameters(List<(string type, string name)> parameters)
		{
			if (parameters.Any() == false)
			{
				return string.Empty;
			}

			var stringBuilder = new StringBuilder();
			stringBuilder.Append(", ");
			foreach (var parameter in parameters)
			{
				stringBuilder.Append($"(nameof({parameter.name}), {parameter.name}), ");
			}

			stringBuilder.Remove(stringBuilder.Length - 2, 2);
			return stringBuilder.ToString();
		}
	}
}