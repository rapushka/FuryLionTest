using Code.Ads;
using Code.GameLoop.Goals.Progress.ProgressObservers;
using Code.UI.GameSettings;
using Code.UI.Windows.Panels;
using Zenject;

namespace Code.UI.Windows.Service
{
	public class WindowsService
	{
		private readonly WindowsChain _windowsChain;
		private readonly Settings _settings;
		private readonly AdsService _adsService;

		[Inject]
		public WindowsService(WindowsChain windowsChain, Settings settings, AdsService adsService)
		{
			_windowsChain = windowsChain;
			_settings = settings;
			_adsService = adsService;
		}

		public void OnVictory() => OpenResultWindowWith(SessionResult.Victory);

		public void OnLose() => _windowsChain.Open<AddExtraActionsWindow>((w) => w.Initialize(this));

		public void Lose() => OpenResultWindowWith(SessionResult.Lose);

		public void OpenSettings() => _windowsChain.Open<SettingsWindow>((w) => w.Initialize(_settings));

		public void OnGoalReached(ProgressObserver progressObserver)
			=> _windowsChain.Open<QuestCompletedWindow>((w) => w.Initialize(progressObserver));

		private void OpenResultWindowWith(SessionResult sessionResult)
			=> _windowsChain.Open<GameResultWindow>((w) => w.Initialize(sessionResult, _adsService));
	}
}