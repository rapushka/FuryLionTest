using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Environment.Bonuses
{
	public class BonusesSpawnCondition
	{
		private readonly IBonusesConfig _bonusesConfig;
		private readonly BonusSpawner _bonusSpawner;
		private readonly Field _field;

		[Inject]
		public BonusesSpawnCondition(IBonusesConfig bonusesConfig, BonusSpawner bonusSpawner, Field field)
		{
			_bonusesConfig = bonusesConfig;
			_bonusSpawner = bonusSpawner;
			_field = field;
		}

		public void OnChainComposed(IEnumerable<Vector2> chain)
		{
			var enumerable = chain as Vector2[] ?? chain.ToArray();
			
			var chainLenght = enumerable.Length;
			var rocketMin = _bonusesConfig.MinChainLenghtForRocket;
			var rocketMax = _bonusesConfig.MaxChainLenghtForRocket;
			var bombMin = _bonusesConfig.MinChainLenghtForBomb;
			var chainedTokensType = _field[enumerable.First()].TokenUnit;

			if (chainLenght.IsBetweenIncluding(rocketMin, rocketMax))
			{
				_bonusSpawner.SpawnHorizontalRocket(chainedTokensType);
			}
			else if (chainLenght >= bombMin)
			{
				_bonusSpawner.SpawnBomb(chainedTokensType);
			}
		}
	}
}