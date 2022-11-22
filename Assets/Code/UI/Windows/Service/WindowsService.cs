using Code.GameLoop.Goals.Progress.ProgressObservers;
using Code.UI.Windows.Panels;
using Zenject;

namespace Code.UI.Windows.Service
{
	public class WindowsService
	{
		private readonly WindowsChain _windowsChain;

		[Inject] public WindowsService(WindowsChain windowsChain) => _windowsChain = windowsChain;

		public void OnVictory() => OpenResultWindowWith(SessionResult.Victory);

		public void OnLose() => _windowsChain.Open<AddExtraActionsWindow>();

		public void Lose() => OpenResultWindowWith(SessionResult.Lose);

		public void OpenSettings() => _windowsChain.Open<SettingsWindow>();

		public void OnGoalReached(ProgressObserver progressObserver)
			=> _windowsChain.Open<QuestCompletedWindow>((w) => w.Initialize(progressObserver));

		private void OpenResultWindowWith(SessionResult sessionResult)
			=> _windowsChain.Open<GameResultWindow>((w) => w.Initialize(sessionResult));
	}
}