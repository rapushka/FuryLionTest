using System.Linq;
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
		private Vector2[] _chain;

		[Inject]
		public BonusSpawner(Field field, SignalBus signalBus)
		{
			_field = field;
			_signalBus = signalBus;
		}

		public void SpawnHorizontalRocket(Vector2[] chain, TokenUnit unit)
			=> Spawn(chain, unit, BonusType.HorizontalRocket);

		public void SpawnBomb(Vector2[] chain, TokenUnit unit)
			=> Spawn(chain, unit, BonusType.Bomb);

		private void Spawn(Vector2[] chain, TokenUnit unit, BonusType bonusType)
		{
			_chain = chain;
			_unit = unit;
			var token = _field.FirstOrDefault(CasualTokenOfRightUnit);
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

		private bool CasualTokenOfRightUnit(Token token)
			=> token.TokenUnit == _unit
			   && token.BonusType == BonusType.None
			   && _chain.Contains(token.transform.position) == false;
	}
}