using System.Collections.Generic;
using System.Linq;
using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Gameplay
{
	public class Chain
	{
		private readonly Field _field;
		private readonly List<Token.Token> _addedTokens;

		private bool _chainComposingInProcess;

		public Chain(Field field)
		{
			_field = field;

			_addedTokens = new List<Token.Token>();
		}

		public void StartComposing(Vector2 position)
		{
			_chainComposingInProcess = true;
			AddTokenAt(position);
		}

		public bool TryAddToken(Vector2 position)
		{
			var tokenCanBeAdded = TokenCanBeAdded(position);
			position.Do(AddTokenAt, @if: tokenCanBeAdded);
			return tokenCanBeAdded;
		}

		public void EndComposing()
		{
			_addedTokens.Clear();
			_chainComposingInProcess = false;
		}

		private bool TokenCanBeAdded(Vector2 position)
			=> _chainComposingInProcess
			   && TokenNotYetAdded(position)
			   && TokenIsFittingType(position)
			   && _field.IsNeighboring(_addedTokens.Last(), _field[position]);

		private void AddTokenAt(Vector2 position) => _addedTokens.Add(_field[position]);

		private bool TokenNotYetAdded(Vector2 position)
			=> _addedTokens.Contains(_field[position]) == false;

		private bool TokenIsFittingType(Vector2 position)
			=> _field[position].TokenType == _addedTokens.First().TokenType;
	}
}