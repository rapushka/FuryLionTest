using System;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.IdComponents;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Gameplay.Tokens
{
	public class TokensPool : IInitializable
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

		public void Initialize()
		{
			for (var i = 0; i < Enum.GetValues(typeof(TokenType)).Length; i++)
			{
				_createdTokens.Add((TokenType)i, new List<Token>());
			}
		}

		public Token CreateTokenOfType(TokenType tokenType, Vector3 position)
			=> HasPooledTokenOfRightType(tokenType, out var token)
				? EnableToken(position, token)
				: CreateNewToken(tokenType, position);

		public void DestroyToken(Token token) => token.gameObject.SetActive(false);

		private static Token EnableToken(Vector3 position, Token token)
		{
			token.gameObject.SetActive(true);
			token.transform.position = position;
			return token;
		}

		private bool HasPooledTokenOfRightType(TokenType tokenType, out Token token)
		{
			token = _createdTokens[tokenType].FirstOrDefault(IsDisabled);
			return token is not null;
		}

		private Token CreateNewToken(TokenType tokenType, Vector3 position)
		{
			var token = InstantiateAtRoot(position, _tokenPrefabForType[tokenType]);
			_createdTokens[tokenType].Add(token);
			return token;
		}

		private static bool IsDisabled(Token token) => token.gameObject.activeInHierarchy == false;

		private Token InstantiateAtRoot(Vector3 position, Token original)
			=> Object.Instantiate(original, position, Quaternion.identity, _tokensRoot.transform);
	}
}