using Code.Analytics;
using Code.Extensions.Analytics;
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
			BindContracts();

			BindSignals();
		}

		private void BindContracts()
		{
			Container
				.BindSingle<AnalyticsEventsInvoker>()
				.BindSingle<AnalyticEventsHandler>()
				;
		}

		private void BindSignals()
		{
			Container
				.BindAnalyticsSignals()
				;
		}
	}
}