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

		private string GenerateSignal(string @namespace, string name, List<(string, string)> p)
			=> SignalsCodeTemplates.Class(@namespace, name, p.GetBaseSignal(), GenerateConstructor(p));

		private string GenerateConstructor(List<(string type, string name)> parameters)
		{
			const int postfixLenght = 2;
			
			if (parameters.Any() == false)
			{
				return string.Empty;
			}
			
			var names = new StringBuilder();
			var constructorParameters = new StringBuilder();
			
			foreach (var parameter in parameters)
			{
				names.Append($"{parameter.name}, ");
				constructorParameters.Append(parameter.ForMethod());
			}

			names.RemoveLasSymbols(postfixLenght);
			constructorParameters.RemoveLasSymbols(postfixLenght);

			return $"public {_currentClassName}({constructorParameters}) : base({names}) {{ }}";
		}
	}
}