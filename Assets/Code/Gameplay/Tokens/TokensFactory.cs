using System;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.IdComponents;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Gameplay.Tokens
{
	public class TokensFactory : IInitializable
	{
		private readonly Dictionary<TokenUnit, Token> _tokenPrefabForType;
		private readonly TokensRoot _tokensRoot;
		private readonly Dictionary<TokenUnit, List<Token>> _createdTokens;

		[Inject]
		public TokensFactory(TokensCollection tokenPrefabForType, TokensRoot tokensRoot)
		{
			_tokenPrefabForType = tokenPrefabForType.AsDictionary();
			_tokensRoot = tokensRoot;

			_createdTokens = new Dictionary<TokenUnit, List<Token>>();
		}

		public void Initialize()
		{
			for (var i = 0; i < Enum.GetValues(typeof(TokenUnit)).Length; i++)
			{
				_createdTokens.Add((TokenUnit)i, new List<Token>());
			}
		}

		public Token CreateTokenForUnit(TokenUnit tokenUnit, Vector3 position)
			=> HasPooledTokenForThisUnit(tokenUnit, out var token)
				? EnableToken(position, token)
				: CreateNewToken(tokenUnit, position);

		public void DestroyToken(Token token) => token.gameObject.SetActive(false);

		private static Token EnableToken(Vector3 position, Token token)
		{
			token.gameObject.SetActive(true);
			token.transform.position = position;
			return token;
		}

		private bool HasPooledTokenForThisUnit(TokenUnit tokenUnit, out Token token)
		{
			token = _createdTokens[tokenUnit].FirstOrDefault(IsDisabled);
			return token is not null;
		}

		private Token CreateNewToken(TokenUnit tokenUnit, Vector3 position)
		{
			var token = InstantiateAtRoot(position, _tokenPrefabForType[tokenUnit]);
			_createdTokens[tokenUnit].Add(token);
			return token;
		}

		private static bool IsDisabled(Token token) => token.gameObject.activeInHierarchy == false;

		private Token InstantiateAtRoot(Vector3 position, Token original)
			=> Object.Instantiate(original, position, Quaternion.identity, _tokensRoot.transform);
	}
}