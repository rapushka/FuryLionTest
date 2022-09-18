using System.Collections.Generic;
using System.Linq;
using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Gameplay
{
	public class Chain
	{
		private readonly Field _field;
		private readonly List<Token> _chainedTokens;

		private bool _chainComposingInProcess;

		public Chain(Field field)
		{
			_field = field;

			_chainedTokens = new List<Token>();
		}

		public void StartComposing(Vector2 position)
		{
			var isNotStartedYet = _chainComposingInProcess == false;
			_chainComposingInProcess = true;
			position.Do(AddTokenAt, @if: isNotStartedYet);
		}

		public bool TryAddToken(Vector2 position)
		{
			var tokenCanBeAdded = TokenCanBeAdded(position);
			position.Do(AddTokenAt, @if: tokenCanBeAdded);
			return tokenCanBeAdded;
		}

		public void EndComposing()
		{
			var isNotEndedYet = _chainComposingInProcess;
			_chainComposingInProcess = false;
			_chainedTokens.Do((c) => c.Clear(), @if: isNotEndedYet);
		}

		private bool TokenCanBeAdded(Vector2 position)
			=> _chainComposingInProcess
			   && TokenNotYetAdded(position)
			   && TokenIsFittingType(position)
			   && IsNeighborForLastToken(position);

		private void AddTokenAt(Vector2 position) => _chainedTokens.Add(_field[position]);

		private bool TokenNotYetAdded(Vector2 position)
			=> _chainedTokens.Contains(_field[position]) == false;

		private bool TokenIsFittingType(Vector2 position)
			=> _field[position].TokenType == _chainedTokens.First().TokenType;

		private bool IsNeighborForLastToken(Vector2 position)
			=> _field.IsNeighboring(_chainedTokens.Last(), _field[position]);
	}
}