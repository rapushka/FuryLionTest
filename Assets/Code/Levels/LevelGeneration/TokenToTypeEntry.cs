using System;
using Code.Gameplay;
using UnityEngine;

namespace Code.Levels.LevelGeneration
{
	[Serializable]
	public class TokenToTypeEntry
	{
		[SerializeField] private TokenType _tokenType;
		[SerializeField] private Token _tokenPrefab;

		public TokenType TokenType => _tokenType;

		public Token TokenPrefab => _tokenPrefab;
	}
}