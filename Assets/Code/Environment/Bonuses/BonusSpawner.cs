using System.Linq;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Infrastructure;
using UnityEngine;
using Zenject;

namespace Code.Environment.Bonuses
{
	public class BonusSpawner
	{
		private readonly Field _field;
		private readonly SignalBus _signalBus;
		private TokenUnit _unit;
		private Token[] _chain;

		[Inject]
		public BonusSpawner(Field field, SignalBus signalBus)
		{
			_field = field;
			_signalBus = signalBus;
		}

		public void SpawnHorizontalRocket(Token[] chain, TokenUnit unit)
			=> Spawn(chain, unit, BonusType.HorizontalRocket);

		public void SpawnBomb(Token[] chain, TokenUnit unit)
			=> Spawn(chain, unit, BonusType.Bomb);

		private void Spawn(Token[] chain, TokenUnit unit, BonusType bonusType)
		{
			_chain = chain;
			_unit = unit;
			var token = _field.Where(CasualTokenOfRightUnit).PickRandom();

			if (token == true)
			{
				token.BonusType = bonusType;
				_signalBus.Fire(new BonusSpawnedSignal(token));
				return;
			}

			BonusCantBeSpawned();
		}

		private bool CasualTokenOfRightUnit(Token token)
			=> token == true
			   && token.TokenUnit == _unit
			   && token.BonusType == BonusType.None
			   && _chain.Contains(token) == false;

		private static void BonusCantBeSpawned()
		{
			Debug.Log
			(
				"not-bonus token of this color is not exist\n"
				+ "in future it will be added to some buffer\n"
				+ "so far so"
			);
		}
	}
}