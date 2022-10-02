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
		private readonly LevelGenerator _levelGenerator;
		private readonly Gravity _gravity;
		private readonly TokensSpawner _spawner;
		private readonly TokensFactory _tokensFactory;
		private readonly ObstacleDestroyer _obstacleDestroyer;

		private Token[,] _tokens;

		[Inject]
		public Field
		(
			LevelGenerator levelGenerator,
			Gravity gravity,
			TokensSpawner spawner,
			TokensFactory tokensFactory,
			ObstacleDestroyer obstacleDestroyer
		)
		{
			_levelGenerator = levelGenerator;
			_gravity = gravity;
			_spawner = spawner;
			_tokensFactory = tokensFactory;
			_obstacleDestroyer = obstacleDestroyer;
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

		public void OnChainComposed(LinkedList<Vector2> chain)
		{
			foreach (var position in chain)
			{
				DestroyTokenAt(position);
				_obstacleDestroyer.CheckNeighbourTokens(_tokens, position, this);
			}

			UpdateField();
		}

		public void DestroyTokenAt(Vector2 position)
		{
			var token = this[position];

			_tokensFactory.DestroyToken(token);
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