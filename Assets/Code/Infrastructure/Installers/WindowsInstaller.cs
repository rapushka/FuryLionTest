using Code.Extensions.DiContainerExtensions;
using Code.Generated.Analytics.Signals;
using Code.Infrastructure.Signals.GameLoop;
using Code.UI.Buttons;
using Code.UI.Windows.Service;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class WindowsInstaller : MonoInstaller
	{
		[SerializeField] private WindowsChain _windowsChain;
		[SerializeField] private RestartButton _restartButton;

		// ReSharper disable Unity.PerformanceAnalysis - метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstance(_windowsChain)
				.BindSingleFromInstance(_restartButton)
				.BindSingle<WindowsService>()
				;

			Container
				.BindSignalTo<GameVictorySignal, WindowsService>((x) => x.OnVictory)
				.BindSignalTo<GameLoseSignal, WindowsService>((x) => x.OnLose)
				.BindSignalTo<SettingsOpenedSignal, WindowsService>((x) => x.OpenSettings)
				;
		}
	}
}