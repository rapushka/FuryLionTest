using System.Collections.Generic;
using System.Linq;
using Code.Levels.LevelGeneration;
using UnityEngine;

namespace Code.Gameplay.Tokens
{
	public class TokensCollection : MonoBehaviour
	{
		[SerializeField] private SerializableTokensCollection _tokens;

		public Dictionary<TokenUnit, Token> InitializedDictionary()
			=> _tokens.Entries.ToDictionary((e) => e.Unit, (e) => e.Prefab);
	}
}