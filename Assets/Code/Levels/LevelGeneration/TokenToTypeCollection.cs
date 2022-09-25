using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay;
using UnityEngine;

namespace Code.Levels.LevelGeneration
{
	[Serializable]
	public class TokenToTypeCollection
	{
		[SerializeField] private List<TokenToTypeEntry> _entries;

		public List<TokenToTypeEntry> Entries => _entries;

		private Dictionary<TokenType, Token> _cashedDictionary;

		private Dictionary<TokenType, Token> Dictionary => _cashedDictionary ??= SerializedArrayToDictionary();

		public Token this[TokenType tokenType] => Dictionary[tokenType];

		private Dictionary<TokenType, Token> SerializedArrayToDictionary()
			=> _entries.ToDictionary((e) => e.Type, (e) => e.Prefab);
	}
}