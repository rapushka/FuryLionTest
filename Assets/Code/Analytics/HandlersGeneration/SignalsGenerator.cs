using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Code.Analytics.GoogleSheetsIntegration;
using Code.Extensions.Generation;

namespace Code.Analytics.HandlersGeneration
{
	public class SignalsGenerator
	{
		private string _currentClassName;

		public void OnDataProcessed(List<AnalyticEventHandler> handlers)
		{
			const string @namespace = "Code.Generated.Analytics.Signals";
			const string postfix = "Signal";
			
			var path = $@"{Directory.GetCurrentDirectory()}\Assets\{@namespace.Replace('.', '\\')}";

			foreach (var entry in handlers)
			{
				_currentClassName = entry.Event + postfix;
				using var file = File.CreateText(path + @$"\{_currentClassName}.cs");

				file.Write(GenerateSignal(@namespace, _currentClassName, entry.Parameters));
			}
		}

		private string GenerateSignal(string @namespace, string className, List<(string, string)> parameters)
			=> @$"// Generated
using Code.Infrastructure.BaseSignals;

namespace {@namespace}
{{
	public class {className} {parameters.GetBaseImmutableSignal()}
	{{
		{GenerateCtor(parameters)}
	}}
}}
";

		private string GenerateCtor(List<(string type, string name)> parameters)
		{
			const int postfixLenght = 2;
			
			if (parameters.Any() == false)
			{
				return string.Empty;
			}
			
			var names = new StringBuilder();
			var @params = new StringBuilder();
			
			foreach (var p in parameters)
			{
				names.Append($"{p.name}, ");
				@params.Append(p.ForMethod());
			}

			names.RemoveLasSymbols(postfixLenght);
			@params.RemoveLasSymbols(postfixLenght);

			return $"public {_currentClassName}({@params}) : base({names}) {{ }}";
		}
	}
}