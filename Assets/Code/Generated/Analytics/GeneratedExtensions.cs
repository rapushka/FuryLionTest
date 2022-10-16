// generated
using Code.Extensions.DiContainerExtensions;
using Code.Generated.Analytics.Signals;
using Zenject;

namespace Code.Generated.Analytics
{
	public static class GeneratedExtensions
	{
		public static DiContainer BindGeneratedHandlers(this DiContainer @this)
		{
			@this
				.BindSignalTo<LevelClosedSignal, AnalyticEventsHandler>((x, v) => x.OnLevelClosed(v.Value1, v.Value2))
				.BindSignalTo<LevelOpenedSignal, AnalyticEventsHandler>((x, v) => x.OnLevelOpened(v.Value))
				.BindSignalTo<SettingsOpenedSignal, AnalyticEventsHandler>((x) => x.OnSettingsOpened)
				.BindSignalTo<MusicChangedSignal, AnalyticEventsHandler>((x, v) => x.OnMusicChanged(v.Value))
				.BindSignalTo<SoundChangedSignal, AnalyticEventsHandler>((x, v) => x.OnSoundChanged(v.Value))
				;

			return @this;
		}
	}
}