using System;
using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay.Tokens;
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

		private Vector2 DirectedBombExplosionRange => _bombExplosionRange * Vector2.one;

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
			=> position.ForX(_fieldOffsetX, _fieldOffsetX + _fieldSizes.x, Destroy);

		private void DestroyRectangleArea(Vector2 position)
		{
			var start = position - DirectedBombExplosionRange;
			var end = position + DirectedBombExplosionRange;

			position.ForX(start.x, end.x, (p) => DestroyExplosionLine(p, start.y, end.y));
		}

		private void DestroyExplosionLine(Vector2 position, float from, float to)
		{
			for (position.y = from; position.y <= to; position.y++)
			{
				Destroy(position);
			}
		}

		private void Destroy(Vector2 position)
		{
			if (IsInBounces(position)
			    && IsDestroyable(_field[position]))
			{
				_field.DestroyTokenAt(position);
			}
		}

		private bool IsInBounces(Vector2 position)
			=> position.x >= 0
			   && position.x < _fieldSizes.x
			   && position.y >= 0
			   && position.y < _fieldSizes.y;

		private static bool IsDestroyable(Token token) => token.TokenUnit is not TokenUnit.Border;
	}
}