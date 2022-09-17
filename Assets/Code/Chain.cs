using System.Collections.Generic;
using System.Linq;
using Code.Workflow.Extensions;
using UnityEngine;

namespace Code
{
	public class Chain
	{
		private readonly Token.Token[] _tokens;

		private bool _chainStarted;
		private readonly List<Token.Token> _addedTokens;

		public Chain(Token.Token[] tokens)
		{
			_tokens = tokens;
			_addedTokens = new List<Token.Token>();
		}

		public void StartComposing(Vector2 position)
		{
			_chainStarted = true;
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
			_chainStarted = false;
		}

		private bool TokenCanBeAdded(Vector2 position)
			=> _chainStarted && TokenNotYetAdded(position) && TokenIsFittingType(position);

		private void AddTokenAt(Vector2 position) => _addedTokens.Add(GetTokenByPosition(position));

		private bool TokenNotYetAdded(Vector2 position)
			=> _addedTokens.Contains(GetTokenByPosition(position)) == false;

		private bool TokenIsFittingType(Vector2 position)
			=> GetTokenByPosition(position).TokenType == _addedTokens.First().TokenType;

		private Token.Token GetTokenByPosition(Vector2 position)
			=> _tokens.First((token) => (Vector2)token.transform.position == position);
	}
}