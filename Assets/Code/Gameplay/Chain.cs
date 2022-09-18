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
			RemoveLastTokenIfPenultimate(nextPosition);
			AddIfNew(nextPosition);
		}

		public void EndComposing()
		{
			var isNotEndedYet = _chainComposingInProcess;
			_chainComposingInProcess = false;
			_chainedTokens.Do((c) => c.Clear(), @if: isNotEndedYet);
			ChainEnded?.Invoke();
		}

		private void AddIfNew(Vector2 position)
		{
			var tokenCanBeAdded = TokenCanBeAdded(position);
			position.Do(AddTokenAt, @if: tokenCanBeAdded);
		}

		private void RemoveLastTokenIfPenultimate(Vector2 nextPosition)
		{
			var isPenultimate = _field[nextPosition] == _chainedTokens.Last.Previous?.Value;
			_chainedTokens.Do((c) => c.RemoveLast(), @if: isPenultimate);
			LastTokenRemoved.Do((e) => e?.Invoke(), @if: isPenultimate);
		}

		private bool TokenCanBeAdded(Vector2 position)
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