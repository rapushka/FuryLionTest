using Code.DataStoring;
using Code.Extensions.DiContainerExtensions;
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
			var coroutinesHandler = Instantiate(_coroutinesHandlerPrefab);
			DontDestroyOnLoad(coroutinesHandler);

			Container
				.BindSingleWithInterfaces<SceneTransfer>()
				.BindSingleFromInstance(_debugLevel)
				.BindInterfaceSingleTo<IStorage, BinaryStorage>()
				.BindSingleFromInstance(coroutinesHandler)
				;

			SignalBusInstaller.Install(Container);

			SubscribeSignals();
		}

		private void SubscribeSignals()
			=> Container
			   .BindSignalTo<GameVictorySignal, SceneTransfer>((x) => x.ToVictoryScene)
			   .BindSignalTo<GameLoseSignal, SceneTransfer>((x) => x.ToLoseScene)
			   .BindSignalTo<SceneLoadedSignal, CoroutinesHandler>((x) => x.OnSceneChanged)
			   ;
	}
}