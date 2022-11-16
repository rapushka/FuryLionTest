using Code.DataStoring;
using Code.Extensions.DiContainerExtensions;
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
		[SerializeField] private WindowsChain _windowsChainPrefab;

		// ReSharper disable Unity.PerformanceAnalysis - метод вызывается только на инициализации
		public override void InstallBindings()
		{
			var coroutinesHandler = InstantiateDontDestroy(_coroutinesHandlerPrefab);
			var windowChain = InstantiateDontDestroy(_windowsChainPrefab);

			Container
				.BindSingleWithInterfaces<SceneTransfer>()
				.BindSingle<WindowsService>()
				.BindSingleFromInstance(_debugLevel)
				.BindInterfaceSingleTo<IStorage, BinaryStorage>()
				.BindSingleFromInstance(coroutinesHandler)
				.BindSingleFromInstance(windowChain)
				;

			SignalBusInstaller.Install(Container);

			SubscribeSignals();
		}

		private static TObject InstantiateDontDestroy<TObject>(TObject prefab)
			where TObject : Object
		{
			var instance = Instantiate(prefab);
			DontDestroyOnLoad(instance);
			return instance;
		}

		private void SubscribeSignals()
			=> Container
			   .BindSignalTo<GameVictorySignal, WindowsService>((x) => x.OnVictory)
			   .BindSignalTo<GameLoseSignal, WindowsService>((x) => x.OnLose)
			   .BindSignalTo<SceneLoadedSignal, CoroutinesHandler>((x) => x.OnSceneChanged);
	}
}