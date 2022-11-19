using System;
using Code.Ads;
using Code.GameLoop.Goals.Progress.ProgressObservers;
using Code.Gameplay.Coins;
using Code.UI.GameSettings;
using Code.UI.Windows.Panels;
using UnityEngine;
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

		public void OnLose() => OpenResultWindowWith(SessionResult.Lose);

		private void OpenResultWindowWith(SessionResult sessionResult)
			=> _windowsChain.Open<GameResultWindow>((w) => Initialize(w, sessionResult));

		private void Initialize(GameResultWindow window, SessionResult sessionResult)
			=> window.Initialize(sessionResult, _adsService);

		public void OpenSettings() => _windowsChain.Open<SettingsWindow>((w) => w.Initialize(_settings));

		public void ShowConfirmPurchaseWindow(int price, Action spawn)
		{
			_windowsChain.Open<ConfirmPurchaseWindow>((w) => w.Initialize(_coins.CoinsCount, price));
			_windowsChain.WindowClose += (r) => OnWindowClose(r, price, spawn);
		}

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

		public void OnGoalReached(ProgressObserver progressObserver)
		{
			Debug.Log($"Window service: goal reached of type {progressObserver.GetType().Name}");
		}
	}
}