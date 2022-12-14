using System;
using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Gameplay.TokensField.GravityBehaviour;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.TokensField
{
	public class Field : IInitializable
	{
		private readonly LevelGenerator _levelGenerator;
		private readonly Gravity _gravity;
		private readonly TokensSpawner _spawner;
		private readonly TokensPool _tokensPool;

		private Token[,] _tokens;

		[Inject]
		public Field(LevelGenerator levelGenerator, Gravity gravity, TokensSpawner spawner, TokensPool tokensPool)
		{
			_levelGenerator = levelGenerator;
			_gravity = gravity;
			_spawner = spawner;
			_tokensPool = tokensPool;
		}

		public void Initialize()
		{
			_tokens = _levelGenerator.Generate();

			UpdateField();
		}

		public Token this[Vector2 position]
		{
			get => _tokens.GetAtVector(position.ToVectorInt());
			private set => _tokens.SetAtVector(position.ToVectorInt(), value);
		}

		public bool Contain(Token token) => _tokens.Contain(token);
		
		public Vector2Int GetIndexesFor(Token token) => _tokens.IndexesOf(token);

		public void DestroyTokensInChain(IEnumerable<Token> chain) => chain.ForEach(DestroyToken);

		public void UpdateField()
		{
			var fieldNeedHandle = true;
			while (fieldNeedHandle)
			{
				ApplyGravity();
				fieldNeedHandle = TrySpawnTokens();
			}
		}

		public void DestroyTokenAt(Vector2Int indexes)
		{
			var token = this[indexes];

			if (token == false)
			{
				return;
			}

			_tokensPool.DestroyToken(token);
			this[indexes] = null;
		}

		public void SwitchTokenAt(Vector2Int indexes, TokenUnit to)
		{
			var gameObjectPosition = this[indexes].transform.position;
			DestroyTokenAt(indexes);
			this[indexes] = _tokensPool.CreateTokenForUnit(to, gameObjectPosition);
		}

		public IEnumerable<Token> Where(Func<Token, bool> predicate) => _tokens.Where(predicate);

		public int Count(Func<Token, bool> predicate) => _tokens.Where(predicate).Count();
		
		public Token GetRandomToken() => _tokens.PickRandom(where: IsColor);

		private bool IsColor(Token t) => t != null && t.TokenUnit.IsColor();

		private void DestroyToken(Token token)
		{
			if (Contain(token))
			{
				DestroyTokenAt(GetIndexesFor(token));
			}
		}

		private void ApplyGravity() => _tokens = _gravity.Apply(_tokens);

		private bool TrySpawnTokens() => _spawner.Spawn(_tokens);
	}
}