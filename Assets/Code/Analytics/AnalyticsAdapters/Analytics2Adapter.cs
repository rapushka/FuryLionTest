using System.Linq;
using Code.Analytics.ThirdPartyAnalytics;

namespace Code.Analytics.AnalyticsAdapters
{
	public class Analytics2Adapter : IAnalytic
	{
		private readonly Analytics2 _analytic;

		public Analytics2Adapter() => _analytic = new Analytics2();

		public void HandleEvent(string eventName, params (string, object)[] @params)
		{
			_analytic.Event(eventName, @params.ToList());
		}
	}
}