using System.Collections.Generic;
using System.Linq;

namespace Code.Analytics.HandlersGeneration.Handler
{
	public class HandlerGenerator : SingleFileGenerator
	{
		protected override string GenerateClass(string @namespace, string className)
			=> HandlerCodeTemplates.Class(@namespace, className, GenerateMethods());

		private string GenerateMethods() => string.Join('\n', HandlersAsTemplates());

		private IEnumerable<string> HandlersAsTemplates() => Handlers.Select(HandlerCodeTemplates.Method);
	}
}