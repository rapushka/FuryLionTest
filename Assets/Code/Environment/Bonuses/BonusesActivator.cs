using System.Collections.Generic;
using Code.Extensions;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Environment.Bonuses
{
	public class BonusesActivator
	{
		private readonly Field _field;
		private readonly Vector2Int _fieldSizes;
		private readonly int _bombExplosionRange;

		[Inject]
		public BonusesActivator(Field field, IFieldConfig fieldConfig, IBonusesConfig bonusesConfig)
		{
			_field = field;
			_fieldSizes = fieldConfig.FieldSizes;
			_bombExplosionRange = bonusesConfig.BombExplosionRange;
		}

		public void OnChainComposed(IEnumerable<Vector2> chain)
		{
			chain.ForEach(HandleToken);
		}

		private void HandleToken(Vector2 position)
		{
			var token = _field[position];
			 
			if (token == false)
			{
				return;
			}

			if (token.BonusType == BonusType.HorizontalRocket)
			{
				DestroyHorizontalLine(position);
			}
			else if (token.BonusType == BonusType.Bomb)
			{
				DestroyRectangleArea(position);
			}
		}

		private void DestroyRectangleArea(Vector2 position)
		{
			var startX = (int)position.x - _bombExplosionRange;
			var endX = (int)position.x + _bombExplosionRange;
			var startY = (int)position.y - _bombExplosionRange;
			var endY = (int)position.y + _bombExplosionRange;

			for (var x = startX; x < endX; x++)
			{
				for (var y = startY; y < endY; y++)
				{
					if (x >= 0
					    && x < _fieldSizes.x
					    && y >= 0
					    && y < _fieldSizes.y)
					{
						_field.DestroyTokenAt(new Vector2(x, y));
					}
				}
			}
		}

		private void DestroyHorizontalLine(Vector2 position)
		{
			var y = position.y;

			for (var x = 0.5f; x < _fieldSizes.x; x++)
			{
				_field.DestroyTokenAt(new Vector2(x, y));
			}
		}
	}
}