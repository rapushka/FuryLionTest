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
		public event Action<LinkedList<Vector2>> ChainEnded;

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
			=> nextPosition.Do
			(
				@if: IsPenultimateToken,
				@true: RemoveLastToken,
				@false: TryAddTokenAt
			);

		private void TryAddTokenAt(Vector2 position) => position.Do(AddTokenAt, @if: TokenShouldBeAdded);

		public void EndComposing()
		{
			var isNotEndedYet = _chainComposingInProcess;
			_chainComposingInProcess = false;
			
			ChainEnded?.Invoke(_chainedTokens);
			
			_chainedTokens.Do((c) => c.Clear(), @if: isNotEndedYet);
		}

		private bool IsPenultimateToken(Vector2 nextPosition)
			=> nextPosition == _chainedTokens.Last?.Previous?.Value;

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
			=> position.Do(AddTokenToChain)
			           .Do(TokenAddedInvoke);

		private void AddTokenToChain(Vector2 position) => _chainedTokens.AddLast(position);

		private void TokenAddedInvoke(Vector2 position) => TokenAdded?.Invoke(position);

		private bool TokenNotYetAdded(Vector2 position)
			=> _chainedTokens.Contains(position) == false;

		private bool TokenIsFittingType(Vector2 position) 
			=> _field[ChainHead].TokenType == _field[position].TokenType;

		private bool IsNeighborForLastToken(Vector2 position)
			=> _field.IsNeighboring(_chainedTokens.Last(), position);
	}
}