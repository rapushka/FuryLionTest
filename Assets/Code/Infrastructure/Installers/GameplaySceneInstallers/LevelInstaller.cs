using Code.Environment;
using Code.Environment.Bonuses;
using Code.Environment.GravityBehaviour;
using Code.Environment.Obstacles;
using Code.Extensions;
using Code.GameLoop;
using Code.GameLoop.Goals.Progress;
using Code.Gameplay;
using Code.Gameplay.ScoreSystem;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.SerializedImplementation;
using Code.Infrastructure.IdComponents;
using Code.Infrastructure.SceneManagement;
using Code.Inner.CustomMonoBehaviours;
using Code.Input;
using Code.UI.GoalViews;
using Code.View;
using Code.View.Animations;
using Code.View.SpritesBehaviour;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.GameplaySceneInstallers
{
	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] private Transform _tokensRoot;
		[SerializeField] private LineRenderer _lineRenderer;
		[SerializeField] private Transform _goalsRootTransform;
		[SerializeField] private CoroutinesHandler _coroutinesHandler;
		
		[SerializeField] private TokensCollection _tokensCollection;
		[SerializeField] private SerializedConfig _serializedConfig;
		[SerializeField] private TokensSpriteSheet _tokensSpriteSheet;

		// ReSharper disable Unity.PerformanceAnalysis метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstanceWithInterfaces(_serializedConfig)
				.BindSingleFromInstance(_tokensCollection)
				.BindSingleFromInstance(new TokensRoot(_tokensRoot))
				.BindSingleFromInstance(_lineRenderer)
				.BindSingleFromInstance(_tokensSpriteSheet)
				.BindSingleFromInstance(new GoalsRoot(_goalsRootTransform))
				.BindSingleFromInstance(_coroutinesHandler)
				.BindSingle<Gravity>()
				.BindSingle<Chain>()
				.BindSingle<ChainLineRenderer>()
				.BindSingle<CompletedChain>()
				.BindSingle<TokensSpawner>()
				.BindSingle<LevelGenerator>()
				.BindSingle<TokensDistanceMeter>()
				.BindSingle<ObstacleDestroyer>()
				.BindSingle<BonusSpawnCondition>()
				.BindSingle<BonusSpawner>()
				.BindSingle<TokenSpritesSwitcher>()
				.BindSingle<BonusesActivator>()
				.BindSingle<ObserversCreator>()
				.BindSingle<TokensViewsMover>()
				.BindSingle<BonusAnimator>()
				.BindSingleWithInterfaces<ActionsRemaining>()
				.BindSingleWithInterfaces<TokensPool>()
				.BindSingleWithInterfaces<Field>()
				.BindSingleWithInterfaces<OverlapMouse>()
				.BindSingleWithInterfaces<InputService>()
				.BindSingleWithInterfaces<GoalsProgress>()
				.BindSingleWithInterfaces<GameCycle>()
				.BindSingleWithInterfaces<Score>()
				.BindSingleWithInterfaces<GoalViewsContainer>()
				;

			SubscribeSignals();
		}

		private void SubscribeSignals()
		{
			Container
				.BindSignalTo<MouseDownSignal, OverlapMouse>((x) => x.EnableOverlapping)
				.BindSignalTo<MouseUpSignal, OverlapMouse>((x) => x.DisableOverlapping)
				.BindSignalTo<MouseUpSignal, Chain>((x) => x.EndComposing)
				.BindSignalTo<TokenHitSignal, Chain>((x, v) => x.NextToken(v.Value))
				.BindSignalTo<TokenClickSignal, Chain>((x, v) => x.StartComposing(v.Value))
				.BindSignalTo<TokenClickSignal, BonusesActivator>((x, v) => x.OnTokenClick(v.Value))
				.BindSignalTo<ChainTokenAddedSignal, ChainLineRenderer>((x, v) => x.OnTokenAdded(v.Value))
				.BindSignalTo<ChainLastTokenRemovedSignal, ChainLineRenderer>((x) => x.OnLastTokenRemoved)
				.BindSignalTo<ChainEndedSignal, ChainLineRenderer>((x, _) => x.OnChainEnded())
				.BindSignalTo<ChainEndedSignal, CompletedChain>((x, v) => x.OnChainEnded(v.Value))
				.BindSignalTo<ChainComposedSignal, BonusesActivator>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, BonusSpawnCondition>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, ObstacleDestroyer>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, Field>((x, v) => x.DestroyTokensInChain(v.Value))
				.BindSignalTo<ChainEndedSignal, Field>((x, _) => x.UpdateField())
				.BindSignalTo<ChainComposedSignal, Score>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<TokensDestroyedByBonusSignal, Score>((x, v) => x.OnTokensDestroyed(v.Value))
				.BindSignalTo<ActionDoneSignal, ActionsRemaining>((x, _) => x.OnActionDone())
				.BindSignalTo<ScoreUpdateSignal, GoalsProgress>((x, v) => x.OnScoreUpdate(v.Value))
				.BindSignalTo<TokenDestroyedSignal, TokenSpritesSwitcher>((x, v) => x.OnTokenDestroyed(v.Value))
				.BindSignalTo<TokenDestroyedSignal, GoalsProgress>((x, v) => x.OnTokenDestroyed(v.Value))
				.BindSignalTo<BonusSpawnedSignal, TokenSpritesSwitcher>((x, v) => x.OnBonusSpawned(v.Value))
				.BindSignalTo<BonusSpawnedSignal, BonusAnimator>((x, v) => x.OnBonusSpawned(v.Value))
				.BindSignalTo<AllGoalsReachedSignal, GameCycle>((x) => x.OnAllGoalsReached)
				.BindSignalTo<ActionsOverSignal, GameCycle>((x) => x.OnActionsOver)
				.BindSignalTo<GameVictorySignal, SceneTransfer>((x) => x.ToVictoryScene)
				.BindSignalTo<GameLoseSignal, SceneTransfer>((x) => x.ToLoseScene)
				;
		}
	}
}