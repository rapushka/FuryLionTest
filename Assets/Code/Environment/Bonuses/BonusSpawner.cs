using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Environment.Bonuses
{
	public class BonusSpawner
	{
		private readonly Field _field;

		[Inject]
		public BonusSpawner(Field field)
		{
			_field = field;
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
					"non-bonus token of this color is not exist\n"
					+ "in future it will be added to some buffer\n"
					+ "so far so"
				);
				return;
			}

			token.BonusType = bonusType;
			// TODO: BonusSpawnedSignal<Vector2> — по которому соотв. класс заменит изображение токена на необходимое
			Debug.Log($"spawn new {token.BonusType} of type {token.TokenUnit} on {token.transform.position}");
		}

		private static bool NotBonusTokenOfRightUnit(Token t, TokenUnit unit)
			=> t.TokenUnit == unit && t.BonusType == BonusType.None;
	}
}