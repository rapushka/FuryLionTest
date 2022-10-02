using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Levels.LevelGeneration
{
	[Serializable]
	public class SerializableTokensCollection
	{
		[SerializeField] private List<TokenToSpawnUnitEntry> _entries;

		public List<TokenToSpawnUnitEntry> Entries => _entries;

		private Dictionary<TokenUnit, Token> _cashedDictionary;

		private Dictionary<TokenUnit, Token> Dictionary => _cashedDictionary ??= SerializedArrayToDictionary();

		public Token this[TokenUnit tokenUnit] => Dictionary[tokenUnit];

		private Dictionary<TokenUnit, Token> SerializedArrayToDictionary()
			=> _entries.ToDictionary((e) => e.Unit, (e) => e.Prefab);
	}
}