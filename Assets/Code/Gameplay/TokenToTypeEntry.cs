using System;
using UnityEngine;

namespace Code.Gameplay
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