using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Environment.Bonuses
{
	public class BonusesSpawner
	{
		private readonly IBonusesConfig _bonusesConfig;

		[Inject]
		public BonusesSpawner(IBonusesConfig bonusesConfig)
		{
			_bonusesConfig = bonusesConfig;
		}

		public void OnChainComposed(IEnumerable<Vector2> chain)
		{
			var chainLenght = chain.Count();
			var rocketMin = _bonusesConfig.MinChainLenghtForRocket;
			var rocketMax = _bonusesConfig.MaxChainLenghtForRocket;
			var bombMin = _bonusesConfig.MinChainLenghtForBomb;

			if (chainLenght.IsBetweenIncluding(rocketMin, rocketMax))
			{
				Debug.Log("в этот момент могла бы быть создана ракета");
			}
			else if (chainLenght >= bombMin)
			{
				Debug.Log("в этот момент могла бы быть создана бомба");
			}
		}
	}
}