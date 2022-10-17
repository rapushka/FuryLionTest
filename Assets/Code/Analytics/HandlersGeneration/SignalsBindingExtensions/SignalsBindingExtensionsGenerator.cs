using System.Collections.Generic;
using System.Linq;

namespace Code.Analytics.HandlersGeneration.SignalsBindingExtensions
{
	public class SignalsBindingExtensionsGenerator : SingleFileGenerator
	{
		private const string NewLineWithSameOffset = "\r\n\t\t\t\t";

		protected override string GenerateClass(string @namespace, string className)
			=> BindingCodeTemplates.Class(@namespace, className, GenerateBindings());

		private string GenerateBindings()
			=> Handlers.Any() == false
				? string.Empty
				: $"@this{NewLineWithSameOffset}{GenerateInvokes()};";

		private string GenerateInvokes() => string.Join(string.Empty, HandlersAsTemplates());

		private IEnumerable<string> HandlersAsTemplates() => Handlers.Select(BindingCodeTemplates.Method);
	}
}