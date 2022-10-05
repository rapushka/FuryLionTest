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
		private readonly Vector2 _fieldOffset;

		[Inject]
		public BonusesActivator(Field field, IFieldConfig fieldConfig, IBonusesConfig bonusesConfig)
		{
			_field = field;
			_fieldSizes = fieldConfig.FieldSizes;
			_bombExplosionRange = bonusesConfig.BombExplosionRange;
			_fieldOffset = fieldConfig.Offset;
		}

		private Vector2 DirectedRange => _bombExplosionRange * Vector2.one;

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
			=> position.ForX(_fieldOffset.x, _fieldOffset.x + _fieldSizes.x, Destroy);

		private void DestroyRectangleArea(Vector2 position)
			=> position.DoubleFor(from: position - DirectedRange, to: position + DirectedRange, Destroy);

		private void Destroy(Vector2 position)
		{
			var token = _field[position];
			if (token == true
			    && IsInFieldBounces(position)
			    && IsDestroyable(token))
			{
				_field.DestroyTokenAt(position);
			}
		}

		private bool IsInFieldBounces(Vector2 position) 
			=> position.IsInBouncesIncluding(_fieldOffset, _fieldSizes - _fieldOffset);

		private static bool IsDestroyable(Token token) => token.TokenUnit is not TokenUnit.Border;
	}
}