using System.Linq;
using Code.Analytics.ThirdPartyAnalytics;

namespace Code.Analytics.AnalyticsAdapters
{
	public class Analytics1Adapter : IAnalytic
	{
		private readonly Analytics1 _analytic;

		public Analytics1Adapter() => _analytic = new Analytics1();

		public void HandleEvent(string eventName, params object[] @params)
		{
			var strings = @params.Select((p) => p.ToString());
			_analytic.sendEvent(eventName, strings.ToArray());
		}
	}
}