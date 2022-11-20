using Code.Analytics;
using Code.Extensions.DiContainerExtensions;
using Code.Generated.Analytics;
using Code.Infrastructure.Signals.GameLoop;
using Zenject;

namespace Code.Extensions.Analytics
{
	public static class AnalyticsHandlersExtensions
	{
		public static DiContainer BindAnalyticsSignals(this DiContainer @this)
		{
			@this
				.BindGeneratedHandlers()
				.BindSignalTo<SceneLoadedSignal, AnalyticsEventsInvoker>((x) => x.OnSceneChanged)
				.BindSignalTo<GameLoseSignal, AnalyticsEventsInvoker>((x) => x.OnGameLose)
				.BindSignalTo<GameVictorySignal, AnalyticsEventsInvoker>((x) => x.OnGameVictory)
				;
			return @this;
		}
	}
}