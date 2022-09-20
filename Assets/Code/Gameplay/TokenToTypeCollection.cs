using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Gameplay
{
	[Serializable]
	public class TokenToTypeCollection
	{
		[SerializeField] private TokenToTypeEntry[] _entries;

		private Dictionary<TokenType, Token> _dictionary;

		private Dictionary<TokenType, Token> Dictionary => _dictionary ??= Initialize();

		public Token this[TokenType tokenType] => Dictionary[tokenType];

		private Dictionary<TokenType, Token> Initialize()
			=> _entries.ToDictionary((e) => e.TokenType, (e) => e.TokenPrefab);
	}
}