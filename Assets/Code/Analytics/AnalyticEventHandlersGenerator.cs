using System.Collections.Generic;
using Code.Analytics.AnalyticsAdapters;
using UnityEditor;
using UnityEngine;

namespace Code.Analytics
{
	public class AnalyticEventHandlersGenerator
	{
		private readonly IEnumerable<IAnalytic> _analytics;

		public AnalyticEventHandlersGenerator()
		{
			_analytics = new List<IAnalytic>
			{
				new Analytics1Adapter(),
				new Analytics2Adapter(),
			};
		}

		public void Initialize()
		{
			foreach (var analytic in _analytics)
			{
				analytic.HandleEvent("Level closed", ("levelIndex", 1), ("result", true));
				analytic.HandleEvent("Level opened", ("levelIndex", 1));
				analytic.HandleEvent("Settings opened");
				analytic.HandleEvent("Music changed", ("value", 0f));
				analytic.HandleEvent("Sound Changed", ("value", 1f));
			}
		}
		
		[MenuItem("Tools/Analytics/Generate handlers")]
		public static void Generate()
		{
			Debug.Log("Generating...");
		}
	}
}