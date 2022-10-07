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

		[Inject]
		public BonusesActivator
			(Field field, IFieldConfig fieldConfig, IBonusesConfig bonusesConfig, SignalBus signalBus)
		{
			_field = field;
			_signalBus = signalBus;
			_fieldSizes = fieldConfig.FieldSizes;
			_bombExplosionRange = bonusesConfig.BombExplosionRange;
		}

		private Vector2Int DirectedRange => _bombExplosionRange * Vector2Int.one;

		public void OnChainComposed(IEnumerable<Token> chain) => chain.ForEach(HandleToken);

		public void OnTokenClick(Token token)
		{
			var b = token.BonusType is BonusType.HorizontalRocket or BonusType.Bomb;
			HandleToken(token);
			if (b)
			{
				_signalBus.Fire<MouseUpSignal>();
			}
		}

		private void HandleToken(Token token)
		{
			if (token == false)
			{
				return;
			}

			if (token.BonusType == BonusType.HorizontalRocket)
			{
				ActivateHorizontalRocket(_field.GetIndexesFor(token));
			}
			else if (token.BonusType == BonusType.Bomb)
			{
				ActivateBomb(_field.GetIndexesFor(token));
			}
		}

		private void ActivateHorizontalRocket(Vector2Int indexes)
			=> indexes.ForX(0, _fieldSizes.x, Destroy);

		private void ActivateBomb(Vector2Int indexes)
		{
			indexes.DoubleFor(from: indexes - DirectedRange, to: indexes + DirectedRange + Vector2Int.one, Destroy);
		}

		private void Destroy(Vector2Int indexes)
		{
			if (IsInFieldBounces(indexes)
			    && _field[indexes] == true
			    && IsDestroyable(_field[indexes]))
			{
				_field.DestroyTokenAt(indexes);
			}
		}

		private bool IsInFieldBounces(Vector2Int indexes)
			=> indexes.IsInBouncesIncluding(Vector2Int.zero, _fieldSizes);

		private static bool IsDestroyable(Token token) => token.TokenUnit is not TokenUnit.Border;
	}
}