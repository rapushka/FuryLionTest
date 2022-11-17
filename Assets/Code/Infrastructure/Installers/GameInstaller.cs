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
	public class GameInstaller : MonoInstaller, IInitializable
	{
		[SerializeField] private Level _debugLevel;
		[SerializeField] private CoroutinesHandler _coroutinesHandlerPrefab;
		[SerializeField] private WindowsChain _windowsChainPrefab;

		// ReSharper disable Unity.PerformanceAnalysis - метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstanceWithInterfaces(this)
				.BindSingleWithInterfaces<SceneTransfer>()
				.BindSingleWithInterfaces<GameStarter>()
				.BindSingle<WindowsService>()
				.BindSingleFromInstance(_debugLevel)
				.BindInterfaceSingleTo<IStorage, BinaryStorage>()
				.BindSinglePrefabAsDontDestroy(_windowsChainPrefab)
				.BindSinglePrefabAsDontDestroy(_coroutinesHandlerPrefab)
				;

			SignalBusInstaller.Install(Container);

			SubscribeSignals();
		}

		public void Initialize()
		{
			// TODO: instead InstantiateDontDestroy
		}

		private void SubscribeSignals()
			=> Container
			   .BindSignalTo<GameVictorySignal, WindowsService>((x) => x.OnVictory)
			   .BindSignalTo<GameLoseSignal, WindowsService>((x) => x.OnLose)
			   .BindSignalTo<SceneLoadedSignal, CoroutinesHandler>((x) => x.OnSceneChanged);
	}
}