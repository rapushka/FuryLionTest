namespace Code.Analytics.HandlersGeneration.SignalsBindingExtensions
{
	public static class BindingCodeTemplates
	{
		private const string NewLineWithSameOffset = "\r\n\t\t\t\t";
		
		public static string Class(string @namespace, string className, string bindings)
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
			{bindings}

			return @this;
		}}
	}}
}}";
		
		public static string Method(string @event, string argsDeclaration, string argsUsage)
		=> $".BindSignalTo<{@event}Signal, "
		   + $"AnalyticEventsHandler>((x{argsDeclaration}) "
		   + $"=> x.On{@event}{argsUsage})"
		   + NewLineWithSameOffset;
	}
}