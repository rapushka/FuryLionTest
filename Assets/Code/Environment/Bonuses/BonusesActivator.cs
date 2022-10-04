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
		private readonly float _fieldOffsetX;

		[Inject]
		public BonusesActivator(Field field, IFieldConfig fieldConfig, IBonusesConfig bonusesConfig)
		{
			_field = field;
			_fieldSizes = fieldConfig.FieldSizes;
			_bombExplosionRange = bonusesConfig.BombExplosionRange;
			_fieldOffsetX = fieldConfig.Offset.x;
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

		private void DestroyHorizontalLine(Vector2 position)
		{
			var y = position.y;

			for (var x = _fieldOffsetX; x < _fieldSizes.x; x++)
			{
				_field.DestroyTokenAt(new Vector2(x, y));
			}
		}

		private void DestroyRectangleArea(Vector2 position)
		{
			var startPosition = position - DirectedBombExplosionRange;
			var endPosition = position + DirectedBombExplosionRange;

			for (var x = startPosition.x; x <= endPosition.x; x++)
			{
				for (var y = startPosition.y; y <= endPosition.y; y++)
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

		private Vector2 DirectedBombExplosionRange => _bombExplosionRange * Vector2.one;
	}
}