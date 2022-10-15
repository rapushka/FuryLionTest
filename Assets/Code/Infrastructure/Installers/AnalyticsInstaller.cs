using System.Collections.Generic;
using Code.Analytics.AnalyticsAdapters;
using Code.Extensions;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class AnalyticsInstaller : MonoInstaller
	{
		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			IEnumerable<IAnalytic> analytics = new List<IAnalytic>
			{
				new Analytics1Adapter(),
				new Analytics2Adapter(),
			};

			Container.BindSingleFromInstance(analytics);
		}
	}
}