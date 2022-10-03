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
			var array = chain as Vector2[] ?? chain.ToArray();
			
			var chainLenght = array.Length;
			var rocketMin = _bonusesConfig.MinChainLenghtForRocket;
			var rocketMax = _bonusesConfig.MaxChainLenghtForRocket;
			var bombMin = _bonusesConfig.MinChainLenghtForBomb;
			var chainColor = _field[array.First()].TokenUnit;

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