using System;
using Code.Ads;
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
		private int _price;
		private Action _spawn;

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
			_spawn = spawn;
			_price = price;
			_windowsChain.Open<ConfirmPurchaseWindow>((w) => w.Initialize(_coins.CoinsCount, price));
			_windowsChain.WindowClose += OnWindowClose;
		}

		private void OnWindowClose(WindowResult result)
		{
			_windowsChain.WindowClose -= OnWindowClose;

			if (result is not WindowResult.Yes)
			{
				return;
			}

			if (_coins.TrySpent(_price))
			{
				_spawn.Invoke();
			}
			else
			{
				// Show not enough coins window
				Debug.Log("Not enough coins");
			}
		}
	}
}