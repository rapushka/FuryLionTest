using System;
using Code.Gameplay.TokensField.Bonuses;
using Code.Infrastructure.Configurations.Interfaces;
using Code.UI.Windows.Service;
using Zenject;

namespace Code.Gameplay.Coins
{
	public class Purchase
	{
		private readonly ICoinsConfig _coinsConfig;
		private readonly WindowsService _windowsService;
		private readonly BonusSpawner _spawner;

		[Inject]
		public Purchase
		(
			ICoinsConfig coinsConfig,
			WindowsService windowsService,
			BonusSpawner spawner
		)
		{
			_coinsConfig = coinsConfig;
			_windowsService = windowsService;
			_spawner = spawner;
		}

		public void BuyHorizontalRocket() => BuyBonus(_coinsConfig.HorizontalRocketPrice, SpawnRocket);

		public void BuyBomb() => BuyBonus(_coinsConfig.BombPrice, SpawnBomb);

		private void SpawnRocket() => _spawner.SpawnHorizontalRocket();

		private void SpawnBomb() => _spawner.SpawnBomb();

		private void BuyBonus(int price, Action spawn) => _windowsService.ShowConfirmBonusPurchaseWindow(price, spawn);
	}
}