using System.Collections;
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
			=> BindingCodeTemplates.Class(@namespace, className, GenerateBindings());

		private string GenerateBindings()
			=> _handlers.Any() == false
				? string.Empty
				: $"@this{NewLineWithSameOffset}{GenerateInvokes()};";

		private string GenerateInvokes()
		{
			var result = new StringBuilder();
			_handlers.ForEach((h) => result.Append(HandlerAsTemplate(h)));
			return result.ToString();
		}

		private string HandlerAsTemplate(AnalyticEventHandler handler)
			=> BindingCodeTemplates.Method
				(handler.Event, GetArgsDeclaration(handler.Parameters), GetArgsUsage(handler.Parameters));

		private string GetArgsDeclaration(IEnumerable<(string type, string name)> parameters)
			=> parameters.Any() ? ", v" : string.Empty;

		private string GetArgsUsage(ICollection parameters)
			=> parameters.Count switch
			{
				0 => string.Empty,
				1 => "(v.Value)",
				_ => GenerateArgsForTwoAndMore(parameters.Count)
			};

		private static string GenerateArgsForTwoAndMore(int parametersCount)
		{
			const int postfixLenght = 2;

			var result = new StringBuilder();
			result.Append('(');

			for (var i = 0; i < parametersCount; i++)
			{
				result.Append($"v.Value{i + 1}, ");
			}

			result.RemoveLasSymbols(postfixLenght);
			result.Append(')');

			return result.ToString();
		}
	}
}