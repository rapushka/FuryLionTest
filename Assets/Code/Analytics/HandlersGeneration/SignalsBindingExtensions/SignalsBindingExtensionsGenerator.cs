using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.Analytics.GoogleSheetsIntegration;

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
			=> BindingCodeTemplates.Class(@namespace, className, GenerateBindings());

		private string GenerateBindings()
			=> _handlers.Any() == false
				? string.Empty
				: $"@this{NewLineWithSameOffset}{GenerateInvokes()};";

		private string GenerateInvokes() => string.Join(string.Empty, HandlersAsTemplates());

		private IEnumerable<string> HandlersAsTemplates() => _handlers.Select(BindingCodeTemplates.Method);
	}
}