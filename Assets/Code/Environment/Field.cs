using System.Collections.Generic;
using Code.Environment.GravityBehaviour;
using Code.Gameplay;
using Code.Extensions;
using UnityEngine;

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

		public void Construct(LevelGenerator levelGenerator, Gravity gravity, TokensSpawner spawner)
			=> (_levelGenerator, _gravity, _spawner) = (levelGenerator, gravity, spawner);

		private void Start()
		{
			_tokens = _levelGenerator.Generate();
			_step = _levelGenerator.Step;

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
			// var tokensWasSpawned = true;
			// while (tokensWasSpawned)
			ApplyGravity();
			TrySpawnTokens();
		}

		private void ApplyGravity() => _tokens = _gravity.Apply(_tokens);

		private bool TrySpawnTokens() => _spawner.Spawn(_tokens);
	}
}