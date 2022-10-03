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

		[Inject]
		public BonusSpawner(Field field, SignalBus signalBus)
		{
			_field = field;
			_signalBus = signalBus;
		}

		public void SpawnHorizontalRocket(TokenUnit unit) => Spawn(unit, BonusType.HorizontalRocket);

		public void SpawnBomb(TokenUnit unit) => Spawn(unit, BonusType.Bomb);

		private void Spawn(TokenUnit unit, BonusType bonusType)
		{
			var token = _field.FirstOrDefault((t) => NotBonusTokenOfRightUnit(t, unit));
			if (token == false)
			{
				Debug.Log
				(
					"not-bonus token of this color is not exist\n"
					+ "in future it will be added to some buffer\n"
					+ "so far so"
				);
				return;
			}

			token.BonusType = bonusType;
			_signalBus.Fire(new BonusSpawnedSignal(token));
		}

		private static bool NotBonusTokenOfRightUnit(Token token, TokenUnit unit)
			=> token.TokenUnit == unit && token.BonusType == BonusType.None;
	}
}