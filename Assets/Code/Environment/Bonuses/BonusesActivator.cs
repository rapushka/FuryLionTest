using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Infrastructure;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Environment.Bonuses
{
	public class BonusesActivator
	{
		private readonly Field _field;
		private readonly SignalBus _signalBus;
		private readonly Vector2Int _fieldSizes;
		private readonly int _bombExplosionRange;
		private readonly Vector2 _fieldOffset;

		[Inject]
		public BonusesActivator
			(Field field, IFieldConfig fieldConfig, IBonusesConfig bonusesConfig, SignalBus signalBus)
		{
			_field = field;
			_signalBus = signalBus;
			_fieldSizes = fieldConfig.FieldSizes;
			_bombExplosionRange = bonusesConfig.BombExplosionRange;
			_fieldOffset = fieldConfig.Offset;
		}

		private Vector2 DirectedRange => _bombExplosionRange * Vector2.one;

		public void OnChainComposed(IEnumerable<Vector2> chain) => chain.ForEach(HandleToken);

		public void OnTokenClick(Vector2 position)
		{
			var b = _field[position].BonusType is BonusType.HorizontalRocket or BonusType.Bomb;
			HandleToken(position);
			if (b)
			{
				_signalBus.Fire<MouseUpSignal>();
			}
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
				ActivateHorizontalRocket(position);
			}
			else if (token.BonusType == BonusType.Bomb)
			{
				ActivateBomb(position);
			}
		}

		private void ActivateHorizontalRocket(Vector2 position)
			=> position.ForX(_fieldOffset.x, _fieldOffset.x + _fieldSizes.x, Destroy);

		private void ActivateBomb(Vector2 position)
			=> position.DoubleFor(from: position - DirectedRange, to: position + DirectedRange, Destroy);

		private void Destroy(Vector2 position)
		{
			if (IsInFieldBounces(position)
			    && _field[position] == true
			    && IsDestroyable(_field[position]))
			{
				_field.DestroyTokenAt(position);
			}
		}

		private bool IsInFieldBounces(Vector2 position)
			=> position.IsInBouncesIncluding(_fieldOffset, _fieldSizes - _fieldOffset);

		private static bool IsDestroyable(Token token) => token.TokenUnit is not TokenUnit.Border;
	}
}