namespace Code.Analytics.HandlersGeneration.Signals
{
	public static class SignalsCodeTemplates
	{
		public static string Class(string @namespace, string className, string baseClass, string constructor)
		=> @$"// Generated
using Code.Infrastructure.BaseSignals;

namespace {@namespace}
{{
	public class {className} {baseClass}
	{{
		{constructor}
	}}
}}
";
	}
}