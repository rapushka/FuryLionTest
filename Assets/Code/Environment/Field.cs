using System;
using System.Collections.Generic;
using System.Linq;
using Code.Environment.GravityBehaviour;
using Code.Gameplay;
using Code.Extensions;
using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class Field : IInitializable
	{
		private readonly LevelGenerator _levelGenerator;
		private readonly Gravity _gravity;
		private readonly TokensSpawner _spawner;
		private readonly TokensPool _tokensPool;

		private Token[,] _tokens;

		[Inject]
		public Field
		(
			LevelGenerator levelGenerator,
			Gravity gravity,
			TokensSpawner spawner,
			TokensPool tokensPool
		)
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

		public void DestroyTokensInChain(IEnumerable<Vector2> chain)
		{
			chain.ForEach(DestroyTokenAt);
		}

		public void UpdateField()
		{
			var fieldHandled = false;
			while (fieldHandled == false)
			{
				ApplyGravity();
				fieldHandled = TrySpawnTokens() == false;
			}
		}

		public void DestroyTokenAt(Vector2 position)
		{
			var token = this[position];

			if (token == false)
			{
				return;
			}

			_tokensPool.DestroyToken(token);
			this[position] = null;
		}

		public void SwitchTokenAt(Vector2 position, TokenUnit to)
		{
			DestroyTokenAt(position);
			this[position] = _tokensPool.CreateTokenForUnit(to, position);
		}

		public IEnumerable<Token> Where(Func<Token, bool> predicate) => _tokens.Where(predicate);

		public int Count(Func<Token, bool> predicate) => _tokens.Where(predicate).Count();

		private void ApplyGravity() => _tokens = _gravity.Apply(_tokens);

		private bool TrySpawnTokens() => _spawner.Spawn(_tokens);
	}
}