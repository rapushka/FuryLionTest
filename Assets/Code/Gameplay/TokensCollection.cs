using System.Collections.Generic;
using System.Linq;
using Code.Levels.LevelGeneration;
using UnityEngine;

namespace Code.Gameplay
{
	public class TokensCollection : MonoBehaviour
	{
		[SerializeField] private TokenToTypeCollection _tokens;

		private Dictionary<TokenType, Token> _dictionary;

		private void OnEnable() => InitializedDictionary();

		public Dictionary<TokenType, Token> InitializedDictionary()
			=> _dictionary ??= _tokens.Entries.ToDictionary((e) => e.Type, (e) => e.Prefab);
	}
}