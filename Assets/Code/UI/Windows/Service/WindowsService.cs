using Code.Ads;
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

		public void OnLose() => OpenResultWindowWith(SessionResult.Lose);

		private void OpenResultWindowWith(SessionResult sessionResult)
			=> _windowsChain.Open<GameResultWindow>((w) => Initialize(w, sessionResult));

		private void Initialize(GameResultWindow window, SessionResult sessionResult)
			=> window.Construct(sessionResult, _adsService);

		public void OpenSettings() => _windowsChain.Open<SettingsWindow>((w) => w.Construct(_settings));

		public void ShowBonusWindow(int price)
		{
			
		}
	}
}