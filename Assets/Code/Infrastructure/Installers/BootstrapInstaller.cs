using Code.Extensions.DiContainerExtensions;
using Code.Infrastructure.Bootstrap;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class BootstrapInstaller : MonoInstaller
	{
		// ReSharper disable Unity.PerformanceAnalysis - метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingle<GameBootstrapper>();
		}
	}
}