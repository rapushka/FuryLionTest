using Code.DataStoring;
using Code.Extensions;
using Code.Infrastructure.ScenesTransfers;
using Code.Infrastructure.Signals.GameLoop;
using Code.Inner.CustomMonoBehaviours;
using Code.Levels;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private Level _debugLevel;
		[SerializeField] private CoroutinesHandler _coroutinesHandlerPrefab;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			var sceneTransfer = new SceneTransfer();
			var coroutines = Container.InstantiatePrefab(_coroutinesHandlerPrefab);

			Container
				.BindSingleFromInstance(sceneTransfer)
				.BindSingleFromInstance(_debugLevel)
				.BindInterfaceSingleTo<IStorage, BinaryStorage>()
				.BindSingleFromInstance(coroutines)
				;

			SignalBusInstaller.Install(Container);

			SubscribeSignals();
		}

		private void SubscribeSignals()
			=> Container
			   .BindSignalTo<GameVictorySignal, SceneTransfer>((x) => x.ToVictoryScene)
			   .BindSignalTo<GameLoseSignal, SceneTransfer>((x) => x.ToLoseScene);
	}
}