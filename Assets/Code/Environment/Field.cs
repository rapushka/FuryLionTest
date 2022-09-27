using System.Collections.Generic;
using Code.Environment.GravityBehaviour;
using Code.Gameplay;
using Code.Extensions;
using Code.Infrastructure;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class Field : MonoBehaviour
	{
		[SerializeField] private int _minTokensCountForChain = 3;

		private LevelGenerator _levelGenerator;
		private float _step;
		private Token[,] _tokens;
		private Gravity _gravity;
		private TokensSpawner _spawner;

		[Inject]
		public void Construct(LevelGenerator generator, Gravity gravity, TokensSpawner spawner, GameBalance balance)
		{
			(_levelGenerator, _gravity, _spawner) = (generator, gravity, spawner);

			_step = balance.Field.Step;
		}

		private void Start()
		{
			_tokens = _levelGenerator.Generate();

			UpdateField();
		}

		public Token this[Vector2 position]
		{
			get => _tokens.GetAtVector(position.ToVectorInt());
			set => _tokens.SetAtVector(position.ToVectorInt(), value);
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

				Destroy(token.gameObject);
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