using Code.DataStoring;
using Code.Extensions.DiContainerExtensions;
using Code.Infrastructure.Bootstrap;
using Code.Infrastructure.ScenesTransfers;
using Code.Infrastructure.Signals.GameLoop;
using Code.Inner.CustomMonoBehaviours;
using Code.Levels;
using Code.UI.Windows.Service;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private Level _debugLevel;
		[SerializeField] private CoroutinesHandler _coroutinesHandlerPrefab;

		// ReSharper disable Unity.PerformanceAnalysis - метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingleWithInterfaces<SceneTransfer>()
				.BindSingleWithInterfaces<GameStarter>()
				.BindSingleFromInstance(_debugLevel)
				.BindInterfaceSingleTo<IStorage, BinaryStorage>()
				.BindSinglePrefabAsDontDestroy(_coroutinesHandlerPrefab)
				;

			SignalBusInstaller.Install(Container);
			SubscribeSignals();
		}

		private void SubscribeSignals()
			=> Container
			   .BindSignalTo<GameVictorySignal, WindowsService>((x) => x.OnVictory)
			   .BindSignalTo<GameLoseSignal, WindowsService>((x) => x.OnLose)
			   .BindSignalTo<SceneLoadedSignal, CoroutinesHandler>((x) => x.OnSceneChanged)
			   .BindSignalTo<RestartSignal, GameStarter>((x) => x.StartGame);
	}
}