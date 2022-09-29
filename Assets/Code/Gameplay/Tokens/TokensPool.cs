using System.Collections.Generic;
using System.Linq;
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
		{
			CreateListForType(tokenType);

			var token = _createdTokens[tokenType].FirstOrDefault(Disabled);

			if (token == null)
			{
				token = InstantiateAtRoot(position, _tokenPrefabForType[tokenType]);
				_createdTokens[tokenType].Add(token);
			}
			else
			{
				token.gameObject.SetActive(true);
				token.transform.position = position;
			}

			return token;
		}

		private bool Disabled(Token t) => t.gameObject.activeInHierarchy == false;

		private void CreateListForType(TokenType tokenType)
		{
			if (_createdTokens.ContainsKey(tokenType) == false)
			{
				_createdTokens.Add(tokenType, new List<Token>());
			}
		}

		private Token InstantiateAtRoot(Vector3 position, Token original)
			=> Object.Instantiate(original, position, Quaternion.identity, _tokensRoot.transform);

		public void DestroyToken(Token token)
		{
			token.gameObject.SetActive(false);
		}
	}
}