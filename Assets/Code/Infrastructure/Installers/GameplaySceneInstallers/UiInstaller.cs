using Code.Extensions.DiContainerExtensions;
using Code.Infrastructure.Signals.ActionsLeftSignals;
using Code.Infrastructure.Signals.Goals;
using Code.UI.BonusesSpawn;
using Code.UI.GameSettings;
using Code.UI.GoalViews;
using Code.View;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.GameplaySceneInstallers
{
	public class UiInstaller : MonoInstaller
	{
		[SerializeField] private OpenSettingsButton _settingsButton;
		[SerializeField] private SettingsWindow _settingsWindow;
		[SerializeField] private SoundSettings _soundSettings;
		[SerializeField] private ScoreView _scoreView;
		[SerializeField] private RemainingActionsView _remainingActionsView;
		[SerializeField] private ReachScoreGoalView _reachScoreGoalViewPrefab;
		[SerializeField] private DestroyTokensGoalView _destroyTokensGoalViewPrefab;
		[SerializeField] private LanguageSelector _languageSelector;
		[SerializeField] private ButtonSpawnHorizontalRocket _buttonSpawnRocket;
		[SerializeField] private ButtonSpawnBomb _buttonSpawnBomb;

		// ReSharper disable Unity.PerformanceAnalysis
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstance(_settingsWindow)
				.BindSingleFromInstance(_soundSettings)
				.BindSingleFromInstance(_settingsButton)
				.BindSingleFromInstance(_scoreView)
				.BindSingleFromInstance(_remainingActionsView)
				.BindSingleFromInstance(_reachScoreGoalViewPrefab)
				.BindSingleFromInstance(_destroyTokensGoalViewPrefab)
				.BindSingleFromInstance(_languageSelector)
				.BindSingleFromInstance(_buttonSpawnRocket)
				.BindSingleFromInstance(_buttonSpawnBomb)
				.BindSingleWithInterfaces<SettingsInitializer>()
				;

			SubscribeSignals();
		}

		private void SubscribeSignals()
		{
			Container
				.BindSignalTo<ScoreUpdateSignal, ScoreView>((x, v) => x.OnScoreUpdate(v.Value))
				.BindSignalTo<ActionsLeftUpdateSignal, RemainingActionsView>((x, v) => x.UpdateView(v.Value))
				;
		}
	}
}