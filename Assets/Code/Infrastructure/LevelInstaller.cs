using Code.Environment;
using Code.Environment.Bonuses;
using Code.Environment.GravityBehaviour;
using Code.Environment.Obstacles;
using Code.Extensions;
using Code.GameCycle;
using Code.GameCycle.Goals.Progress;
using Code.Gameplay;
using Code.Gameplay.ScoreSystem;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.SerializedImplementation;
using Code.Infrastructure.IdComponents;
using Code.Infrastructure.SceneManagement;
using Code.Input;
using Code.Levels;
using Code.View;
using Code.View.SpritesBehaviour;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] private TokensRoot _tokensRoot;
		[SerializeField] private LineRenderer _lineRenderer;
		[SerializeField] private TokensCollection _tokensCollection;
		[SerializeField] private SerializedConfig _serializedConfig;
		[SerializeField] private Level _debugLevel;
		[SerializeField] private ScoreView _scoreView;
		[SerializeField] private RemainingActionsView _remainingActionsView;
		[SerializeField] private SceneField _loseScene;
		[SerializeField] private TokensSpriteSheet _tokensSpriteSheet;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstanceWithInterfaces(_serializedConfig)
				.BindSingleFromInstance(_tokensCollection)
				.BindSingleFromInstance(_tokensRoot)
				.BindSingleFromInstance(_debugLevel)
				.BindSingleFromInstance(_lineRenderer)
				.BindSingleFromInstance(_scoreView)
				.BindSingleFromInstance(_remainingActionsView)
				.BindSingleFromInstance(_loseScene)
				.BindSingleFromInstance(_tokensSpriteSheet)
				.BindSingle<Gravity>()
				.BindSingle<Chain>()
				.BindSingle<ChainView>()
				.BindSingle<CompletedChain>()
				.BindSingle<TokensSpawner>()
				.BindSingle<LevelGenerator>()
				.BindSingle<SceneTransfer>()
				.BindSingle<TokensDistanceMeter>()
				.BindSingle<ObstacleDestroyer>()
				.BindSingle<BonusesSpawnCondition>()
				.BindSingle<BonusSpawner>()
				.BindSingle<TokenSpritesSwitcher>()
				.BindSingle<BonusesActivator>()
				.BindSingle<GoalsProgress>()
				.BindSingle<ObserversFactory>()
				.BindSingleWithInterfaces<Score>()
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
				.BindSignalTo<ChainComposedSignal, BonusesActivator>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, BonusesSpawnCondition>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, ObstacleDestroyer>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, Field>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, Score>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, ActionsRemaining>((x, _) => x.OnChainComposed())
				.BindSignalTo<ScoreUpdateSignal, ScoreView>((x, v) => x.OnScoreUpdate(v.Value))
				.BindSignalTo<ScoreUpdateSignal, GoalsProgress>((x, v) => x.OnScoreUpdate(v.Value))
				.BindSignalTo<RemainingActionsUpdateSignal, RemainingActionsView>
					((x, v) => x.OnRemainingActionsUpdateSignal(v.Value))
				.BindSignalTo<LevelLostSignal, SceneTransfer>((x) => x.ToLoseScene)
				.BindSignalTo<BonusActiveChangedSignal, TokenSpritesSwitcher>((x, v) => x.OnBonusSpawned(v.Value))
				;
		}
	}
}