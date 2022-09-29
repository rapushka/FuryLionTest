using System.Collections.Generic;
using Code.Environment.GravityBehaviour;
using Code.Gameplay;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Infrastructure;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class Field : IInitializable
	{
		private readonly LevelGenerator _levelLevelGenerator;
		private readonly float _step;
		private readonly Gravity _gravity;
		private readonly TokensSpawner _spawner;
		private readonly int _minTokensCountForChain;

		private Token[,] _tokens;

		[Inject]
		public Field
		(
			LevelGenerator levelGenerator,
			Gravity gravity,
			TokensSpawner spawner,
			Configuration.ChainParameters chainParameters,
			Configuration.FieldParameters fieldParameters
		)
		{
			_levelLevelGenerator = levelGenerator;
			_gravity = gravity;
			_spawner = spawner;

			_step = fieldParameters.Step;
			_minTokensCountForChain = chainParameters.MinTokensCountForChain;
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

		public bool IsNeighboring(Vector2 firstPosition, Vector2 secondPosition)
			=> firstPosition.DistanceTo(secondPosition).AsAbs().LessThanOrEqualTo(_step);

		public void OnChainEnded(LinkedList<Vector2> chain)
		{
			if (chain.Count < _minTokensCountForChain)
			{
				return;
			}

			foreach (var position in chain)
			{
				var token = this[position];

				Object.Destroy(token.gameObject);
				this[position] = null;
			}

			UpdateField();
		}

		private void UpdateField()
		{
			var tokensWasSpawned = true;
			while (tokensWasSpawned)
			{
				ApplyGravity();
				tokensWasSpawned = TrySpawnTokens();
			}
		}

		private void ApplyGravity() => _tokens = _gravity.Apply(_tokens);

		private bool TrySpawnTokens() => _spawner.Spawn(_tokens);
	}
}