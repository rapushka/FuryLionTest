using System.Collections.Generic;
using Code.Analytics.AnalyticsAdapters;
using Zenject;

namespace Code.Analytics
{
	public class AnalyticsMock : IInitializable
	{
		private readonly IEnumerable<IAnalytic> _analytics;

		[Inject]
		public AnalyticsMock(IEnumerable<IAnalytic> analytics)
		{
			_analytics = analytics;
		}

		public void Initialize()
		{
			foreach (var analytic in _analytics)
			{
				analytic.HandleEvent("Level closed", 1, true);
				analytic.HandleEvent("Level opened", 1);
				analytic.HandleEvent("Settings opened");
				analytic.HandleEvent("Music changed", 0f);
				analytic.HandleEvent("Sound Changed", 1f);
			}
		}
	}
}