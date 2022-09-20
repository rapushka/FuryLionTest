using System;
using Code.Gameplay;
using Code.Extensions;
using UnityEngine;

namespace Code.Environment
{
	public class Field : MonoBehaviour
	{
		[SerializeField] private float _step;

		private LevelGenerator _levelGenerator;
		private Token[,] _tokens;

		public void Construct(LevelGenerator levelGenerator) => _levelGenerator = levelGenerator;

		private void Start() => _tokens = _levelGenerator.Generate();

		public Token this[Vector2 position]
			=> _tokens.First((token) => (Vector2)token.transform.position == position);

		public bool IsNeighboring(Vector2 firstPosition, Vector2 secondPosition)
		{
			var deltaPosition = firstPosition - secondPosition;
			
			return MathF.Abs(deltaPosition.x) <= _step
			       && MathF.Abs(deltaPosition.y) <= _step;
		}
	}
}