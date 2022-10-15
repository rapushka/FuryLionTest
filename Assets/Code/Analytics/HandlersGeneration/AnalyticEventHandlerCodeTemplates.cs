namespace Code.Analytics.HandlersGeneration
{
	public static class AnalyticEventHandlerCodeTemplates
	{
		public static string Class(string @namespace, string className, string handlers)
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

		public static string Method((string action, string @event, string methodParams, string invokeParams) items) 
			=> Method(items.action, items.@event, items.methodParams, items.invokeParams);

		private static string Method
			(string action, string @event, string parametersMethod, string parametersInvoke)
			=> $@"		// Action: {action}
		public void On{@event}({parametersMethod})
		{{
			_analytics.HandleEvent(""{@event}""{parametersInvoke});
		}}
";
	}
}