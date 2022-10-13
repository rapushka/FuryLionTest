namespace Code.Analytics.AnalyticsAdapters
{
	public interface IAnalytic
	{
		void HandleEvent(string eventName, object[] @params);
	}
}