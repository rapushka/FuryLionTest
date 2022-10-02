using System.Collections.Generic;
using System.Linq;
using Code.Levels.LevelGeneration;
using UnityEngine;

namespace Code.Gameplay.Tokens
{
	public class TokensCollection : MonoBehaviour
	{
		[SerializeField] private SerializableTokensCollection _tokens;

		private Dictionary<TokenUnit, Token> _dictionary;

		private void OnEnable() => InitializedDictionary();

		public Dictionary<TokenUnit, Token> InitializedDictionary()
			=> _dictionary ??= _tokens.Entries.ToDictionary((e) => e.Unit, (e) => e.Prefab);
	}
}