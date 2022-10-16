using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Code.Analytics.GoogleSheetsIntegration;
using Code.Extensions.Generation;

namespace Code.Analytics.HandlersGeneration.SignalsBindingExtensions
{
	public class SignalsBindingExtensionsGenerator
	{
		private const string NewLineWithSameOffset = "\r\n\t\t\t\t";

		private List<AnalyticEventHandler> _handlers;

		public void OnDataProcessed(List<AnalyticEventHandler> handlers)
		{
			_handlers = handlers;

			const string @namespace = "Code.Generated.Analytics";
			const string className = "GeneratedExtensions";

			var path = $@"{Directory.GetCurrentDirectory()}\Assets\{@namespace.Replace('.', '\\')}";
			using var file = File.CreateText(path + @$"\{className}.cs");

			var code = GenerateCode(@namespace, className);

			file.Write(code);
		}

		private string GenerateCode(string @namespace, string className)
			=> @$"// generated
using Code.Extensions.DiContainerExtensions;
using Code.Generated.Analytics.Signals;
using Zenject;

namespace {@namespace}
{{
	public static class {className}
	{{
		public static DiContainer BindGeneratedHandlers(this DiContainer @this)
		{{
			{GenerateBindings()}

			return @this;
		}}
	}}
}}";

		private string GenerateBindings()
		{
			return _handlers.Any() == false
				? string.Empty
				: $"@this{NewLineWithSameOffset}{GenerateInvokes()};";
		}

		private string GenerateInvokes()
		{
			var result = new StringBuilder();

			foreach (var handler in _handlers)
			{
				result.Append(HandlerAsTemplate(handler));
			}

			return result.ToString();
		}

		private string HandlerAsTemplate(AnalyticEventHandler handler)
			=> $".BindSignalTo<{handler.Event}Signal, "
			   + $"AnalyticEventsHandler>((x{GetArgsDeclaration(handler.Parameters)}) "
			   + $"=> x.On{handler.Event}{GetArgsUsage(handler.Parameters)})"
			   + NewLineWithSameOffset;

		private string GetArgsDeclaration(IEnumerable<(string type, string name)> parameters)
			=> parameters.Any() ? ", v" : string.Empty;

		private string GetArgsUsage(List<(string type, string name)> parameters)
		{
			const int postfixLenght = 2;
			var count = parameters.Count;

			if (count == 0)
			{
				return string.Empty;
			}

			if (count == 1)
			{
				return "(v.Value)";
			}

			var result = new StringBuilder();
			result.Append('(');

			for (var i = 0; i < count; i++)
			{
				result.Append($"v.Value{i + 1}, ");
			}

			result.RemoveLasSymbols(postfixLenght);
			result.Append(')');

			return result.ToString();
		}
	}
}