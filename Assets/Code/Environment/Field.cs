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
		{
			get
			{
				var indexes = ToIndexes(position);
				return _tokens[indexes.x, indexes.y];
			}
		}

		public bool IsNeighboring(Vector2 firstPosition, Vector2 secondPosition)
		{
			// TODO: ToIndexes(position)

			var deltaPosition = firstPosition - secondPosition;

			return MathF.Abs(deltaPosition.x) <= _step
			       && MathF.Abs(deltaPosition.y) <= _step;
		}

		private Vector2Int ToIndexes(Vector2 position)
		{
			position -= _levelGenerator.Offset;
			position /= _levelGenerator.Step;
			position.y = Mathf.Abs(position.y - (_tokens.GetLength(0) - 1));
			(position.x, position.y) = (position.y, position.x);

			return position.ToIntVector();
		}
	}
}