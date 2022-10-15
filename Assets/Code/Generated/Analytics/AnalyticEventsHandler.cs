// Generated
using Code.Analytics;

namespace Code.Generated.Analytics
{
	public class AnalyticEventsHandler
	{
		private readonly AnalyticsCollection _analytics = new();

		// Action: Когда загрылось окно уровня
		public void OnLevelClosed(int levelIndex, bool result)
		{
			_analytics.HandleEvent("LevelClosed", (nameof(levelIndex), levelIndex), (nameof(result), result));
		}

		// Action: Когда открылось окно уровня
		public void OnLevelOpened(int levelIndex)
		{
			_analytics.HandleEvent("LevelOpened", (nameof(levelIndex), levelIndex));
		}

		// Action: Момент открытия окна настроек
		public void OnSettingsOpened()
		{
			_analytics.HandleEvent("SettingsOpened");
		}

		// Action: Когда изменились настройки музыки
		public void OnMusicChanged(float value)
		{
			_analytics.HandleEvent("MusicChanged", (nameof(value), value));
		}

		// Action: Когда изменились настроки звука
		public void OnSoundChanged(float value)
		{
			_analytics.HandleEvent("SoundChanged", (nameof(value), value));
		}


	}
}
