using Code.Analytics;
using Code.Extensions.DiContainerExtensions;
using Code.Generated.Analytics;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class AnalyticsInstaller : MonoInstaller
	{
		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingle<AnalyticsEventsInvoker>()
				.BindSingle<AnalyticEventsHandler>()
				;

			SubscribeSignals();
		}

		private void SubscribeSignals()
		{
			Container.BindGeneratedHandlers();
		}
	}
}