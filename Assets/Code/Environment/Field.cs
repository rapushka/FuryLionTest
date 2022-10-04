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
		private readonly TokensFactory _tokensFactory;

		private Token[,] _tokens;

		[Inject]
		public Field
		(
			LevelGenerator levelGenerator,
			Gravity gravity,
			TokensSpawner spawner,
			TokensFactory tokensFactory
		)
		{
			_levelGenerator = levelGenerator;
			_gravity = gravity;
			_spawner = spawner;
			_tokensFactory = tokensFactory;
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

		public void OnChainComposed(IEnumerable<Vector2> chain)
		{
			chain.ForEach(DestroyTokenAt);
			UpdateField();
		}

		public void DestroyTokenAt(Vector2 position)
		{
			var token = this[position];

			if (token == false)
			{
				return;
			}

			_tokensFactory.DestroyToken(token);
			this[position] = null;
		}

		public void SwitchTokenAt(Vector2 position, TokenUnit to)
		{
			DestroyTokenAt(position);
			this[position] = _tokensFactory.CreateTokenForUnit(to, position);
		}

		public IEnumerable<Token> Where(Func<Token, bool> predicate) => _tokens.Where(predicate);

		public int Count(Func<Token, bool> predicate) => _tokens.Where(predicate).Count();

		private void UpdateField()
		{
			var fieldHandled = false;
			while (fieldHandled == false)
			{
				ApplyGravity();
				fieldHandled = TrySpawnTokens() == false;
			}
		}

		private void ApplyGravity() => _tokens = _gravity.Apply(_tokens);

		private bool TrySpawnTokens() => _spawner.Spawn(_tokens);
	}
}