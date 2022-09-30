using Code.Environment;
using Code.Environment.GravityBehaviour;
using Code.Extensions;
using Code.GameCycle;
using Code.Gameplay;
using Code.Gameplay.ScoreSystem;
using Code.Gameplay.Tokens;
using Code.Infrastructure.IdComponents;
using Code.Input;
using Code.Levels;
using Code.View;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] private TokensRoot _tokensRoot;
		[SerializeField] private LineRenderer _lineRenderer;
		[SerializeField] private TokensCollection _tokens;
		[SerializeField] private Configuration _configuration;
		[SerializeField] private Level _debugLevel;
		[SerializeField] private TMPro.TextMeshProUGUI _scoreText;
		[SerializeField] private RemainingActionsView _remainingActionsView;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			var dictionaryTokensToType = _tokens.InitializedDictionary();

			Container
				.BindSingleFromInstance(_tokensRoot)
				.BindSingleFromInstance(_debugLevel)
				.BindSingleFromInstance(dictionaryTokensToType)
				.BindSingleFromInstance(_lineRenderer)
				.BindSingleFromInstance(_configuration)
				.BindSingleFromInstance(_configuration.Field)
				.BindSingleFromInstance(_configuration.Input)
				.BindSingleFromInstance(_configuration.Chain)
				.BindSingleFromInstance(_configuration.Score)
				.BindSingleFromInstance(_scoreText)
				.BindSingleFromInstance(_remainingActionsView)
				.BindSingle<Gravity>()
				.BindSingle<Chain>()
				.BindSingle<ChainView>()
				.BindSingle<CompletedChain>()
				.BindSingle<TokensSpawner>()
				.BindSingle<LevelGenerator>()
				.BindSingle<Score>()
				.BindSingle<ScoreView>()
				.BindSingle<SceneTransfer>()
				.BindSingleWithInterfaces<ActionsRemaining>()
				.BindSingleWithInterfaces<TokensPool>()
				.BindSingleWithInterfaces<Field>()
				.BindSingleWithInterfaces<OverlapMouse>()
				.BindSingleWithInterfaces<InputService>()
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
				.BindSignalTo<ChainTokenAddedSignal, ChainView>((x, v) => x.OnTokenAdded(v.Value))
				.BindSignalTo<ChainLastTokenRemovedSignal, ChainView>((x) => x.OnLastTokenRemoved)
				.BindSignalTo<ChainEndedSignal, ChainView>((x, _) => x.OnChainEnded())
				.BindSignalTo<ChainEndedSignal, CompletedChain>((x, v) => x.OnChainEnded(v.Value))
				.BindSignalTo<ChainComposedSignal, Field>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, Score>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, ActionsRemaining>((x, _) => x.OnChainComposed())
				.BindSignalTo<ScoreUpdateSignal, ScoreView>((x, v) => x.OnScoreUpdate(v.Value))
				.BindSignalTo<RemainingActionsUpdateSignal, RemainingActionsView>
					((x, v) => x.OnRemainingActionsUpdateSignal(v.Value))
				.BindSignalTo<LevelLostSignal, SceneTransfer>((x) => x.ToLoseScene)
				;
		}
	}
}