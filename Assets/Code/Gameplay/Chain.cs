using System;
using System.Collections.Generic;
using System.Linq;
using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Gameplay
{
	public class Chain
	{
		private readonly Field _field;
		private readonly LinkedList<Token> _chainedTokens;

		private bool _chainComposingInProcess;

		public event Action<Vector2> TokenAdded;
		public event Action LastTokenRemoved;
		public event Action ChainEnded;

		public Chain(Field field)
		{
			_field = field;

			_chainedTokens = new LinkedList<Token>();
		}

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
			=> _field[nextPosition] == _chainedTokens.Last.Previous?.Value;

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
			_chainedTokens.AddLast(_field[position]);
			TokenAdded?.Invoke(position);
		}

		private bool TokenNotYetAdded(Vector2 position)
			=> _chainedTokens.Contains(_field[position]) == false;

		private bool TokenIsFittingType(Vector2 position)
			=> _field[position].TokenType == _chainedTokens.First().TokenType;

		private bool IsNeighborForLastToken(Vector2 position)
			=> _field.IsNeighboring(_chainedTokens.Last(), _field[position]);
	}
}