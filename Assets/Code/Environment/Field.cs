using System.Collections.Generic;
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
		private readonly LevelGenerator _levelLevelGenerator;
		private readonly Gravity _gravity;
		private readonly TokensSpawner _spawner;
		private readonly TokensPool _tokensPool;

		private Token[,] _tokens;

		[Inject]
		public Field(LevelGenerator levelGenerator, Gravity gravity, TokensSpawner spawner, TokensPool tokensPool)
		{
			_levelLevelGenerator = levelGenerator;
			_gravity = gravity;
			_spawner = spawner;
			_tokensPool = tokensPool;
		}

		public void Initialize()
		{
			_tokens = _levelLevelGenerator.Generate();

			UpdateField();
		}

		public Token this[Vector2 position]
		{
			get => _tokens.GetAtVector(position.ToVectorInt());
			private set => _tokens.SetAtVector(position.ToVectorInt(), value);
		}

		public void OnChainComposed(LinkedList<Vector2> chain)
		{
			foreach (var position in chain)
			{
				DestroyTokenAt(position);
			}

			UpdateField();
		}

		public void DestroyTokenAt(Vector2 position)
		{
			var token = this[position];

			_tokensPool.DestroyToken(token);
			this[position] = null;
		}

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