using System;
using Code.Ads;
using Code.GameLoop.Goals.Progress.ProgressObservers;
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
		public WindowsService(WindowsChain windowsChain, Settings settings, AdsService adsService, CoinsCounter coins)
		{
			_windowsChain = windowsChain;
			_settings = settings;
			_adsService = adsService;
			_coins = coins;
		}

		public void OnVictory() => OpenResultWindowWith(SessionResult.Victory);

		public void OnLose() => _windowsChain.Open<AddExtraActionsWindow>((w) => w.Initialize(this));

		public void Lose() => OpenResultWindowWith(SessionResult.Lose);

		public void OpenSettings() => _windowsChain.Open<SettingsWindow>((w) => w.Initialize(_settings));

		public void ShowConfirmBonusPurchaseWindow(int price, Action spawn)
			=> ShowConfirmPurchaseWindow(price, (r) => OnWindowClose(r, price, spawn));

		public void ShowConfirmPurchaseWindow(int price, Action<WindowResult> callback)
		{
			_windowsChain.Open<ConfirmPurchaseWindow>((w) => w.Initialize(_coins.CoinsCount, price));
			_windowsChain.WindowClose += callback;
		}

		public void OnGoalReached(ProgressObserver progressObserver)
			=> _windowsChain.Open<QuestCompletedWindow>((w) => w.Initialize(progressObserver));

		private void OpenResultWindowWith(SessionResult sessionResult)
			=> _windowsChain.Open<GameResultWindow>((w) => Initialize(w, sessionResult));

		private void Initialize(GameResultWindow window, SessionResult sessionResult)
			=> window.Initialize(sessionResult, _adsService);

		private void OnWindowClose(WindowResult result, int price, Action spawn)
		{
			if (result is WindowResult.Yes)
			{
				BuyBonus(price, spawn);
			}
		}

		private void BuyBonus(int price, Action spawn)
		{
			if (_coins.TrySpent(price))
			{
				spawn.Invoke();
			}
			else
			{
				_windowsChain.Open<NotEnoughMoneyWindow>();
			}
		}

		public void Close() => _windowsChain.Close();
	}
}