using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Signals.Tokens;
using Code.Inner.CustomMonoBehaviours;
using Code.Inner.RootContainers;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Gameplay.Tokens
{
	public class TokensPool : IInitializable
	{
		private readonly Dictionary<TokenUnit, Token> _tokenPrefabForType;
		private readonly TokensRoot _root;
		private readonly SignalBus _signalBus;
		private readonly CoroutinesHandler _coroutines;
		private readonly Dictionary<TokenUnit, List<Token>> _createdTokens;

		[Inject]
		public TokensPool(TokensCollection tokens, TokensRoot root, SignalBus signalBus, CoroutinesHandler coroutines)
		{
			_tokenPrefabForType = tokens.AsDictionary();
			_root = root;
			_signalBus = signalBus;
			_coroutines = coroutines;

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
			_coroutines.StartRoutine(FadeRoutine(token));
			_signalBus.Fire(new TokenDestroyedSignal(token));
		}

		private static IEnumerator FadeRoutine(Component token)
		{
			const float fadeDuration = 0.3f;
			var tokenTransform = token.transform;

			if (tokenTransform == true)
			{
				var initialScale = tokenTransform.localScale;
				
				var tweenCore = tokenTransform.DOScale(Vector3.zero, fadeDuration);
				yield return new WaitForSeconds(fadeDuration);
				tweenCore.Kill();
				
				tokenTransform.localScale = initialScale;
			}

			token.gameObject.SetActive(false);
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
			=> Object.Instantiate(original, position, Quaternion.identity, _root.Transform);
	}
}