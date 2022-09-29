using System.Collections.Generic;
using Code.Infrastructure.IdComponents;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Tokens
{
	public class TokensPool
	{
		private readonly Dictionary<TokenType, Token> _tokenPrefabForType;
		private readonly TokensRoot _tokensRoot;
		private readonly Dictionary<TokenType, List<Token>> _createdTokens;
		
		[Inject]
		public TokensPool(Dictionary<TokenType, Token> tokenPrefabForType, TokensRoot tokensRoot)
		{
			_tokenPrefabForType = tokenPrefabForType;
			_tokensRoot = tokensRoot;
			_createdTokens = new Dictionary<TokenType, List<Token>>();
		}

		public Token CreateTokenOfType(TokenType tokenType, Vector3 position) 
			=> Object.Instantiate(_tokenPrefabForType[tokenType], position, Quaternion.identity, _tokensRoot.transform);

		public void DestroyToken(Token token) => Object.Destroy(token.gameObject);
	}
}