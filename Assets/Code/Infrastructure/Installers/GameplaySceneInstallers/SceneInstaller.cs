using Code.Extensions;
using Code.Infrastructure.Signals.Chain;
using Code.Inner.CustomMonoBehaviours;
using Code.Inner.RootContainers;
using Code.View;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.GameplaySceneInstallers
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField] private Transform _tokensRootTransform;
		[SerializeField] private Transform _goalsRootTransform;
		[SerializeField] private LineRenderer _lineRenderer;
		[SerializeField] private CoroutinesHandler _coroutinesHandler;
		
		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstance(new TokensRoot(_tokensRootTransform))
				.BindSingleFromInstance(new GoalsRoot(_goalsRootTransform))
				.BindSingleFromInstance(_lineRenderer)
				.BindSingleFromInstance(_coroutinesHandler)
				.BindSingle<ChainLineRenderer>()
				;

			SubscribeSignals();
		}

		private void SubscribeSignals()
		{
			Container
				.BindSignalTo<ChainTokenAddedSignal, ChainLineRenderer>((x, v) => x.OnTokenAdded(v.Value))
				.BindSignalTo<ChainLastTokenRemovedSignal, ChainLineRenderer>((x) => x.OnLastTokenRemoved)
				.BindSignalTo<ChainEndedSignal, ChainLineRenderer>((x, _) => x.OnChainEnded())
				;
		}
	}
}