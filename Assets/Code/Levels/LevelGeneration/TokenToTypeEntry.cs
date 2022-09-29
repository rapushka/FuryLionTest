using System;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Levels.LevelGeneration
{
	[Serializable]
	public class TokenToTypeEntry
	{
		[SerializeField] private TokenType _type;
		[SerializeField] private Token _prefab;

		public TokenType Type => _type;

		public Token Prefab => _prefab;
	}
}