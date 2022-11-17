using Code.Extensions.DiContainerExtensions;
using Code.UI.Windows.Service;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class WindowsInstaller : MonoInstaller
	{
		[SerializeField] private WindowsChain _windowsChainPrefab;

		// ReSharper disable Unity.PerformanceAnalysis - метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSinglePrefabAsDontDestroy(_windowsChainPrefab)
				.BindSingle<WindowsService>()
				;
		}
	}
}