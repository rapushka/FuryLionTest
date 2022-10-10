using Code.DataStoring;
using Code.Extensions;
using Code.Infrastructure.ScenesTransfers;
using Code.Infrastructure.Signals.GameLoop;
using Code.Levels;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private Level _debugLevel;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			var sceneTransfer = new SceneTransfer();

			Container
				.BindSingleFromInstance(sceneTransfer)
				.BindSingleFromInstance(_debugLevel)
				.BindInterfaceSingleTo<IStorage, BinaryStorage>()
				;

			SignalBusInstaller.Install(Container);

			SubscribeSignals();
		}

		private void SubscribeSignals()
		{
			Container
				.BindSignalTo<GameVictorySignal, SceneTransfer>((x) => x.ToVictoryScene)
				.BindSignalTo<GameLoseSignal, SceneTransfer>((x) => x.ToLoseScene)
				;
		}
	}
}