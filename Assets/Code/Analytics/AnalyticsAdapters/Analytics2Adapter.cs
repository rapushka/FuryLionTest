using System.Linq;
using Code.Analytics.ThirdPartyAnalytics;

namespace Code.Analytics.AnalyticsAdapters
{
	public class Analytics2Adapter : IAnalytic
	{
		private readonly Analytics2 _analytic;

		public Analytics2Adapter() => _analytic = new Analytics2();

		public void HandleEvent(string eventName, params object[] @params)
		{
			var tuples = @params.Select((p) => (p.ToString(), x: p));
			_analytic.Event(eventName, tuples.ToList());
		}
	}
}