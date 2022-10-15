namespace Code.Analytics.HandlersGeneration
{
	public static class CodeTemplates
	{
		public static string AnalyticEventHandlerClass(string @namespace, string className, string handlers)
			=> @$"// Generated
using Code.Analytics;

namespace {@namespace}
{{
	public class {className}
	{{
		private readonly AnalyticsCollection _analytics = new();

{handlers}
	}}
}}
";

		public static string AnalyticEventHandlerMethod
			(string action, string @event, string parametersMethod, string parametersInvoke)
			=> $@"		// Action: {action}
		public void On{@event}({parametersMethod})
		{{
			_analytics.HandleEvent(""{@event}""{parametersInvoke});
		}}
";
	}
}