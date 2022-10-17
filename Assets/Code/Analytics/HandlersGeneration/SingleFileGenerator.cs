using System.Collections.Generic;
using System.IO;
using Code.Analytics.GoogleSheetsIntegration;

namespace Code.Analytics.HandlersGeneration
{
	public abstract class SingleFileGenerator
	{
		protected List<AnalyticEventHandler> Handlers;

		public void OnDataProcessed(List<AnalyticEventHandler> handlers)
		{
			Handlers = handlers;

			const string @namespace = "Code.Generated.Analytics";
			const string className = "GeneratedExtensions";

			var path = $@"{Directory.GetCurrentDirectory()}\Assets\{@namespace.Replace('.', '\\')}";
			using var file = File.CreateText(path + @$"\{className}.cs");

			var code = GenerateClass(@namespace, className);
			file.Write(code);
		}

		protected abstract string GenerateClass(string @namespace, string className);
	}
}