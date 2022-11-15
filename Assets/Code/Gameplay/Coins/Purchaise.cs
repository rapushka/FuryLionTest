using Code.Gameplay.TokensField.Bonuses;
using Code.Infrastructure.Configurations.Interfaces;
using Zenject;

namespace Code.Gameplay.Coins
{
	public class Purchase
	{
		private readonly CoinsCounter _coins;
		private readonly BonusSpawner _spawner;
		private readonly ICoinsConfig _coinsConfig;

		[Inject]
		public Purchase(CoinsCounter coins, BonusSpawner spawner, ICoinsConfig coinsConfig)
		{
			_coins = coins;
			_spawner = spawner;
			_coinsConfig = coinsConfig;
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
	}
}