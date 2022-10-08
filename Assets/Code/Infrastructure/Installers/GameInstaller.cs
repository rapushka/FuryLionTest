using Code.Extensions;
using Code.GameLoop.Goals.Progress;
using Code.Infrastructure.SceneManagement;
using Code.Infrastructure.Signals.ActionsLeftSignals;
using Code.Infrastructure.Signals.GameLoop;
using Code.Infrastructure.Signals.Goals;
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
				.BindSingleWithInterfaces<GameCycle>()
				;
			
			SignalBusInstaller.Install(Container);

			SubscribeSignals();
		}

		private void SubscribeSignals()
		{
			Container
				.BindSignalTo<GameVictorySignal, SceneTransfer>((x) => x.ToVictoryScene)
				.BindSignalTo<GameLoseSignal, SceneTransfer>((x) => x.ToLoseScene)
				.BindSignalTo<AllGoalsReachedSignal, GameCycle>((x) => x.OnAllGoalsReached)
				.BindSignalTo<ActionsOverSignal, GameCycle>((x) => x.OnActionsOver)
				;
		}
	}
}