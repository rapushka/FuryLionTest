using Code.Environment;
using Code.Environment.GravityBehaviour;
using Code.Extensions;
using Code.Gameplay;
using Code.Infrastructure.IdComponents;
using Code.Input;
using Code.Levels;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] private TokensRoot _tokensRoot;
		[SerializeField] private LineRenderer _lineRenderer;
		[SerializeField] private TokensCollection _tokens;
		[SerializeField] private GameBalance _balance;
		[SerializeField] private Level _debugLevel;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			var dictionaryTokensToType = _tokens.InitializedDictionary();

			Container
				.BindSingleFromInstance(_tokensRoot)
				.BindSingleFromInstance(_debugLevel)
				.BindSingleFromInstance(dictionaryTokensToType)
				.BindSingleFromInstance(_lineRenderer)
				.BindSingleFromInstance(_balance)
				.BindSingle<Gravity>()
				.BindSingle<Chain>()
				.BindSingle<ChainRenderer>()
				.BindSingle<TokensSpawner>()
				.BindSingleWithInterfaces<Field>()
				.BindSingleWithInterfaces<OverlapMouse>()
				.BindSingleWithInterfaces<InputService>()
				.BindSingleWithInterfaces<LevelGenerator>()
				;

			SubscribeSignals();
		}

		private void SubscribeSignals()
		{
			SignalBusInstaller.Install(Container);

			Container
				.BindSignalTo<MouseDownSignal, OverlapMouse>((x) => x.EnableOverlapping)
				.BindSignalTo<MouseUpSignal, OverlapMouse>((x) => x.DisableOverlapping)
				.BindSignalTo<MouseUpSignal, Chain>((x) => x.EndComposing)
				.BindSignalTo<TokenHitSignal, Chain>((x, v) => x.NextToken(v.Value))
				.BindSignalTo<TokenClickSignal, Chain>((x, v) => x.StartComposing(v.Value))
				.BindSignalTo<ChainTokenAddedSignal, ChainRenderer>((x, v) => x.OnTokenAdded(v.Value))
				.BindSignalTo<ChainLastTokenRemovedSignal, ChainRenderer>((x) => x.OnLastTokenRemoved)
				.BindSignalTo<ChainEndedSignal, ChainRenderer>((x, v) => x.OnChainEnded(v.Value))
				.BindSignalTo<ChainEndedSignal, Field>((x, v) => x.OnChainEnded(v.Value))
				;
		}
	}
}