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
		[SerializeField] private SceneField _gameplayScene;
		[SerializeField] private SceneField _victoryScene;
		[SerializeField] private SceneField _loseScene;
		[SerializeField] private Level _debugLevel;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			var sceneTransfer = new SceneTransfer(_gameplayScene, _victoryScene, _loseScene);

			Container
				.BindSingleFromInstance(sceneTransfer)
				.BindSingleFromInstance(_debugLevel)
				.BindSingle<BinaryStorage>()
				.BindInterfaceSingleTo<IStorage, BinaryStorage>()
				;

			Container.Bind<IStorage>().To<BinaryStorage>().AsSingle();

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