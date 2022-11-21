using System;
using Code.Gameplay.TokensField.Bonuses;
using Code.Infrastructure.Configurations.Interfaces;
using Code.UI.Windows.Panels;
using Code.UI.Windows.Service;
using Zenject;

namespace Code.Gameplay.Coins
{
	public class PurchaseBonus
	{
		private readonly ICoinsConfig _coinsConfig;
		private readonly WindowsChain _windowsChain;
		private readonly BonusSpawner _spawner;

		[Inject]
		public PurchaseBonus
		(
			ICoinsConfig coinsConfig,
			WindowsChain windowsChain,
			BonusSpawner spawner
		)
		{
			_coinsConfig = coinsConfig;
			_windowsChain = windowsChain;
			_spawner = spawner;
		}

		public void BuyHorizontalRocket() => BuyBonus(_coinsConfig.HorizontalRocketPrice, SpawnRocket);

		public void BuyBomb() => BuyBonus(_coinsConfig.BombPrice, SpawnBomb);

		private void SpawnRocket() => _spawner.SpawnHorizontalRocket();

		private void SpawnBomb() => _spawner.SpawnBomb();

		private void BuyBonus(int price, Action spawn)
			=> _windowsChain.Open<ConfirmPurchaseWindow>((w) => w.Initialize(price), (result) => Spawn(result, spawn));

		private static void Spawn(WindowResult result, Action spawn)
		{
			if (result is WindowResult.Yes)
			{
				spawn.Invoke();
			}
		}
	}
}