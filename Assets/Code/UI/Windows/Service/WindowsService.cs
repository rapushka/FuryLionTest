using Code.Ads;
using Code.Gameplay.Coins;
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
		private readonly CoinsCounter _coins;

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
			=> window.Initialize(sessionResult, _adsService);

		public void OpenSettings() => _windowsChain.Open<SettingsWindow>((w) => w.Initialize(_settings));

		public void ShowConfirmPurchaseWindow(int price)
		{
			_windowsChain.Open<ConfirmPurchaseWindow>((w) => w.Initialize(price, _coins.CoinsCount));
		}
	}
}