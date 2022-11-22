using Code.Extensions.DiContainerExtensions;
using Code.GameLoop;
using Code.GameLoop.Goals.Progress;
using Code.Gameplay;
using Code.Gameplay.Coins;
using Code.Gameplay.ScoreSystem;
using Code.Gameplay.Tokens;
using Code.Gameplay.TokensField;
using Code.Gameplay.TokensField.Bonuses;
using Code.Gameplay.TokensField.GravityBehaviour;
using Code.Gameplay.TokensField.Obstacles;
using Code.Infrastructure.Configurations.SerializedImplementation;
using Code.Infrastructure.Signals.ActionsLeftSignals;
using Code.Infrastructure.Signals.Bonuses;
using Code.Infrastructure.Signals.Chain;
using Code.Infrastructure.Signals.Coins;
using Code.Infrastructure.Signals.Goals;
using Code.Infrastructure.Signals.Input;
using Code.Infrastructure.Signals.Tokens;
using Code.Input;
using Code.UI.GoalViews;
using Code.View.Animations;
using Code.View.SpritesBehaviour;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.GameplaySceneInstallers
{
	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] private TokensCollection _tokensCollection;
		[SerializeField] private SerializedConfig _serializedConfig;
		[SerializeField] private TokensSpriteSheet _tokensSpriteSheet;

		// ReSharper disable Unity.PerformanceAnalysis - метод вызывается только на инициализации
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstanceWithInterfaces(_serializedConfig)
				.BindSingleFromInstance(_tokensCollection)
				.BindSingleFromInstance(_tokensSpriteSheet)
				.BindSingle<Gravity>()
				.BindSingle<Chain>()
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
				.BindSingleWithInterfaces<Score>()
				.BindSingleWithInterfaces<GoalViewsContainer>()
				.BindSingleWithInterfaces<GameCycle>()
				.BindSingleWithInterfaces<CoinsCounter>()
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
				.BindSignalTo<TokenPressSignal, Chain>((x, v) => x.StartComposing(v.Value))
				.BindSignalTo<TokenClickSignal, BonusesActivator>((x, v) => x.OnTokenClick(v.Value))
				.BindSignalTo<ChainEndedSignal, CompletedChain>((x, v) => x.OnChainEnded(v.Value))
				.BindSignalTo<ChainEndedSignal, Field>((x, _) => x.UpdateField())
				.BindSignalTo<ChainComposedSignal, BonusesActivator>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, BonusSpawnCondition>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, ObstacleDestroyer>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<ChainComposedSignal, Field>((x, v) => x.DestroyTokensInChain(v.Value))
				.BindSignalTo<ChainComposedSignal, Score>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<TokensDestroyedByBonusSignal, Score>((x, v) => x.OnTokensDestroyed(v.Value))
				.BindSignalTo<ChainComposedSignal, CoinsCounter>((x, v) => x.OnChainComposed(v.Value))
				.BindSignalTo<TokensDestroyedByBonusSignal, CoinsCounter>((x, v) => x.OnTokensDestroyed(v.Value))
				.BindSignalTo<ActionDoneSignal, ActionsRemaining>((x, _) => x.OnActionDone())
				.BindSignalTo<ScoreUpdateSignal, GoalsProgress>((x, v) => x.OnScoreUpdate(v.Value))
				.BindSignalTo<TokenDestroyedSignal, TokenSpritesSwitcher>((x, v) => x.OnTokenDestroyed(v.Value))
				.BindSignalTo<TokenDestroyedSignal, GoalsProgress>((x, v) => x.OnTokenDestroyed(v.Value))
				.BindSignalTo<BonusSpawnedSignal, TokenSpritesSwitcher>((x, v) => x.OnBonusSpawned(v.Value))
				.BindSignalTo<BonusSpawnedSignal, BonusAnimator>((x, v) => x.OnBonusSpawned(v.Value))
				.BindSignalTo<AllGoalsReachedSignal, GameCycle>((x) => x.OnAllGoalsReached)
				.BindSignalTo<ActionsOverSignal, GameCycle>((x) => x.OnActionsOver)
				.BindSignalTo<ActionsBoughtSignal, ActionsRemaining>((x, v) => x.OnActionsBought(v.Value))
				.BindSignalTo<ActionsBoughtSignal, GameCycle>((x) => x.OnActionsBought)
				;
		}
	}
}