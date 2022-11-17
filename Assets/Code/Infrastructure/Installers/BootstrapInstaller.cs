using Code.Extensions.DiContainerExtensions;
using Code.Infrastructure.Bootstrap;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class BootstrapInstaller : MonoInstaller
	{
		[SerializeField] private GameBootstrapper _bootstrapper;

		// ReSharper disable Unity.PerformanceAnalysis - метод вызывается только на инициализации
		public override void InstallBindings()
		{
			
			
			Container
				.BindSingleFromInstanceWithInterfaces(_bootstrapper)
				;
		}
	}
}