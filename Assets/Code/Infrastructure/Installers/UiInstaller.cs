using Code.Extensions;
using Code.UI.Settings;
using Code.View;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class UiInstaller : MonoInstaller
	{
		[SerializeField] private OpenSettingsButton _settingsButton;
		[SerializeField] private SettingsWindow _settings;
		[SerializeField] private ScoreView _scoreView;
		[SerializeField] private RemainingActionsView _remainingActionsView;


		// ReSharper disable Unity.PerformanceAnalysis
		public override void InstallBindings()
		{
			Container
				.BindSingleFromInstance(_settings)
				.BindSingleFromInstance(_settingsButton)
				.BindSingleFromInstance(_scoreView)
				.BindSingleFromInstance(_remainingActionsView)
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