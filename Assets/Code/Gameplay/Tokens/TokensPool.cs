using System;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Signals.Tokens;
using Code.Inner.RootContainers;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Gameplay.Tokens
{
	public class TokensPool : IInitializable
	{
		private readonly Dictionary<TokenUnit, Token> _tokenPrefabForType;
		private readonly TokensRoot _tokensRoot;
		private readonly SignalBus _signalBus;
		private readonly Dictionary<TokenUnit, List<Token>> _createdTokens;

		[Inject]
		public TokensPool(TokensCollection tokenPrefabForType, TokensRoot tokensRoot, SignalBus signalBus)
		{
			_tokenPrefabForType = tokenPrefabForType.AsDictionary();
			_tokensRoot = tokensRoot;
			_signalBus = signalBus;

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
				? EnableTokenAt(position, token)
				: CreateNewTokenAt(position, tokenUnit);

		public void DestroyToken(Token token)
		{
			token.gameObject.SetActive(false);

			_signalBus.Fire(new TokenDestroyedSignal(token));
		}

		private static Token EnableTokenAt(Vector3 position, Token token)
		{
			token.transform.position = position;
			token.gameObject.SetActive(true);
			return token;
		}

		private bool HasPooledTokenForThisUnit(TokenUnit tokenUnit, out Token token)
		{
			token = _createdTokens[tokenUnit].FirstOrDefault(IsDisabled);
			return token is not null;
		}

		private Token CreateNewTokenAt(Vector3 position, TokenUnit tokenUnit)
		{
			var token = InstantiateAtRoot(position, _tokenPrefabForType[tokenUnit]);
			_createdTokens[tokenUnit].Add(token);
			return token;
		}

		private static bool IsDisabled(Token token) => token.gameObject.activeInHierarchy == false;

		private Token InstantiateAtRoot(Vector3 position, Token original)
			=> Object.Instantiate(original, position, Quaternion.identity, _tokensRoot.Transform);
	}
}