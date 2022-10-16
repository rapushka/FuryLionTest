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
			if (_handlers.Any() == false)
			{
				return string.Empty;
			}

			return $@"@this
				{GenerateInvokes()}
				;";
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
			   + $"=> x.On{handler.Event}{GetArgsUsage(handler.Parameters)})\n";

		private string GetArgsDeclaration(List<(string type, string name)> parameters)
		{
			var count = parameters.Count;

			if (count == 0)
			{
				return string.Empty;
			}
			if (count == 1)
			{
				return ", v";
			}
			
			var result = new StringBuilder();
			for (var i = 0; i < count; i++)
			{
				result.Append($", v{i + 1}");
			}

			return result.ToString();
		}

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