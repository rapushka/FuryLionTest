using System.Collections.Generic;
using Code.Analytics.AnalyticsAdapters;
using Code.Extensions;

namespace Code.Analytics
{
	public class AnalyticsCollection : IAnalytic
	{
		private readonly IEnumerable<IAnalytic> _analytics;

		public AnalyticsCollection()
		{
			_analytics = new List<IAnalytic>
			{
				new Analytics1Adapter(),
				new Analytics2Adapter(),
			};
		}
		
		public void HandleEvent(string eventName, params (string, object)[] @params) 
			=> _analytics.ForEach((a) => a.HandleEvent(eventName, @params));
	}
}