using Code.Gameplay.TokensField.Bonuses;
using Code.Infrastructure.Configurations.Interfaces;
using Code.UI.Windows.Service;
using Zenject;

namespace Code.Gameplay.Coins
{
	public class Purchase
	{
		private readonly CoinsCounter _coins;
		private readonly BonusSpawner _spawner;
		private readonly ICoinsConfig _coinsConfig;
		private readonly WindowsService _windowsService;

		[Inject]
		public Purchase
		(
			CoinsCounter coins,
			BonusSpawner spawner,
			ICoinsConfig coinsConfig,
			WindowsService windowsService
		)
		{
			_coins = coins;
			_spawner = spawner;
			_coinsConfig = coinsConfig;
			_windowsService = windowsService;
		}

		public void BuyHorizontalRocket()
		{
			if (_coins.TrySpent(_coinsConfig.HorizontalRocketPrice))
			{
				_spawner.SpawnHorizontalRocket();
			}
		}

		public void BuyBomb()
		{
			if (_coins.TrySpent(_coinsConfig.BombPrice))
			{
				_spawner.SpawnBomb();
			}
		}

		public void BuyBonus(int price)
		{
			_windowsService.ShowBonusWindow(price);
		}
	}
}