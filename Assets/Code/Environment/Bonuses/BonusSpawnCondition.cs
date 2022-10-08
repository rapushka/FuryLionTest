using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.Interfaces;
using Zenject;

namespace Code.Environment.Bonuses
{
	public class BonusSpawnCondition
	{
		private readonly IBonusesConfig _bonusesConfig;
		private readonly BonusSpawner _bonusSpawner;

		[Inject]
		public BonusSpawnCondition(IBonusesConfig bonusesConfig, BonusSpawner bonusSpawner)
		{
			_bonusesConfig = bonusesConfig;
			_bonusSpawner = bonusSpawner;
		}

		public void OnChainComposed(IEnumerable<Token> chain)
		{
			var array = chain as Token[] ?? chain.ToArray();
			
			var chainLenght = array.Length;
			var rocketMin = _bonusesConfig.MinChainLenghtForRocket;
			var rocketMax = _bonusesConfig.MaxChainLenghtForRocket;
			var bombMin = _bonusesConfig.MinChainLenghtForBomb;
			
			var chainColor = array.First().TokenUnit;
			
			if (chainLenght.IsBetweenIncluding(rocketMin, rocketMax))
			{
				_bonusSpawner.SpawnHorizontalRocket(array, chainColor);
			}
			else if (chainLenght >= bombMin)
			{
				_bonusSpawner.SpawnBomb(array, chainColor);
			}
		}
	}
}