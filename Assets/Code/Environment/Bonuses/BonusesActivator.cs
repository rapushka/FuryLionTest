using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.Interfaces;
using Code.Infrastructure.Signals.ActionsLeftSignals;
using Code.Infrastructure.Signals.Tokens;
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

		private int _destroyedTokensCountPerAction;

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

		public void OnChainComposed(IEnumerable<Token> chain)
		{
			chain.ForEach(HandleToken);
			InvokeTokensDestroyed();
		}

		public void OnTokenClick(Token token)
		{
			if (TryHandleToken(token) == false)
			{
				return;
			}

			InvokeTokensDestroyed();
			_signalBus.Fire<ActionDoneSignal>();
		}

		private void InvokeTokensDestroyed()
		{
			_signalBus.Fire(new TokensDestroyedByBonusSignal(_destroyedTokensCountPerAction));
			_destroyedTokensCountPerAction = 0;
		}

		private void HandleToken(Token token) => TryHandleToken(token);

		private bool TryHandleToken(Token token)
		{
			if (token == false)
			{
				return false;
			}

			if (token.BonusType is BonusType.HorizontalRocket)
			{
				ActivateHorizontalRocket(_field.GetIndexesFor(token));
				return true;
			}

			if (token.BonusType is BonusType.Bomb)
			{
				ActivateBomb(_field.GetIndexesFor(token));
				return true;
			}

			return false;
		}

		private void ActivateHorizontalRocket(Vector2Int indexes)
			=> indexes.ForX(0, _fieldSizes.x, Destroy);

		private void ActivateBomb(Vector2Int indexes) 
			=> indexes.DoubleFor(from: indexes - DirectedRange, to: indexes + DirectedRange + Vector2Int.one, Destroy);

		private void Destroy(Vector2Int indexes)
		{
			if (IsInFieldBounces(indexes)
			    && _field[indexes] == true
			    && IsDestroyable(_field[indexes]))
			{
				_field.DestroyTokenAt(indexes);
				_destroyedTokensCountPerAction++;
			}
		}

		private bool IsInFieldBounces(Vector2Int indexes)
			=> indexes.IsInBouncesIncluding(Vector2Int.zero, _fieldSizes - Vector2Int.one);

		private static bool IsDestroyable(Token token) => token.TokenUnit is not TokenUnit.Border;
	}
}