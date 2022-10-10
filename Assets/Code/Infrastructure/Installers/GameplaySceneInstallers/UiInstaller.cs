using Code.Extensions;
using Code.Infrastructure.Signals.ActionsLeftSignals;
using Code.Infrastructure.Signals.Goals;
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
		[SerializeField] private SettingsWindow _settings;
		[SerializeField] private ScoreView _scoreView;
		[SerializeField] private RemainingActionsView _remainingActionsView;
		[SerializeField] private ReachScoreGoalView _reachScoreGoalViewPrefab;
		[SerializeField] private DestroyTokensGoalView _destroyTokensGoalViewPrefab;

		// ReSharper disable Unity.PerformanceAnalysis
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstance(_settings)
				.BindSingleFromInstance(_settingsButton)
				.BindSingleFromInstance(_scoreView)
				.BindSingleFromInstance(_remainingActionsView)
				.BindSingleFromInstance(_reachScoreGoalViewPrefab)
				.BindSingleFromInstance(_destroyTokensGoalViewPrefab)
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