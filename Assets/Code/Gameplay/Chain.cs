using System;
using System.Collections.Generic;
using System.Linq;
using Code.Environment;
using Code.Extensions;
using UnityEngine;

namespace Code.Gameplay
{
	public class Chain
	{
		private readonly Field _field;
		private readonly LinkedList<Vector2> _chainedTokens;

		private bool _chainComposingInProcess;

		public event Action<Vector2> TokenAdded;
		public event Action LastTokenRemoved;
		public event Action ChainEnded;

		public Chain(Field field)
		{
			_field = field;

			_chainedTokens = new LinkedList<Vector2>();
		}

		private Vector2 ChainHead => _chainedTokens.First();

		public void StartComposing(Vector2 position)
		{
			var isNotStartedYet = _chainComposingInProcess == false;
			_chainComposingInProcess = true;
			position.Do(AddTokenAt, @if: isNotStartedYet);
		}

		public void NextToken(Vector2 nextPosition)
		{
			var isPenultimate = IsPenultimate(nextPosition);
			nextPosition.Do(RemoveLastToken, @if: isPenultimate);
			nextPosition.Do(AddTokenAt, @if: isPenultimate == false && TokenShouldBeAdded(nextPosition));
		}

		public void EndComposing()
		{
			var isNotEndedYet = _chainComposingInProcess;
			_chainComposingInProcess = false;
			_chainedTokens.Do((c) => c.Clear(), @if: isNotEndedYet);
			ChainEnded?.Invoke();
		}

		private bool IsPenultimate(Vector2 nextPosition)
			=> nextPosition == _chainedTokens.Last.Previous?.Value;

		private void RemoveLastToken(Vector2 nextPosition)
		{
			_chainedTokens.RemoveLast();
			LastTokenRemoved?.Invoke();
		}

		private bool TokenShouldBeAdded(Vector2 position)
			=> _chainComposingInProcess
			   && TokenNotYetAdded(position)
			   && TokenIsFittingType(position)
			   && IsNeighborForLastToken(position);

		private void AddTokenAt(Vector2 position)
		{
			_chainedTokens.AddLast(position);
			TokenAdded?.Invoke(position);
		}

		private bool TokenNotYetAdded(Vector2 position)
			=> _chainedTokens.Contains(position) == false;

		private bool TokenIsFittingType(Vector2 position)
			=> _field[position].TokenType == _field[ChainHead].TokenType;

		private bool IsNeighborForLastToken(Vector2 position)
			=> _field.IsNeighboring(_chainedTokens.Last(), position);
	}
}