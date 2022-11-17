using Code.Extensions.DiContainerExtensions;
using Code.UI.Buttons;
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
			var windowsChain = _windowsChainPrefab.InstantiateDontDestroy();

			Container
				.BindSingleFromInstance(windowsChain)
				.BindSingleFromInstance(windowsChain.GetComponentInChildren<RestartButton>())
				.BindSingle<WindowsService>()
				;
		}
	}
}