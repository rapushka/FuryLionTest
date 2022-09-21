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
				return _tokens.First((token) => IsMatching(token, position));

				var indexes = ToIndexes(position);
				return _tokens[indexes.x, indexes.y];
			}
		}

		public bool IsNeighboring(Vector2 firstPosition, Vector2 secondPosition)
		{
			// ToIndexes(position)

			var deltaPosition = firstPosition - secondPosition;

			Debug.Log($"Neighboring = {MathF.Abs(deltaPosition.x) <= _step && MathF.Abs(deltaPosition.y) <= _step}");

			return MathF.Abs(deltaPosition.x) <= _step
			       && MathF.Abs(deltaPosition.y) <= _step;
		}

		private static bool IsMatching(Component token, Vector2 position)
		{
			return token
			       && (Vector2)token.transform.position == position;
		}

		private Vector2Int ToIndexes(Vector2 position)
		{
			position -= _levelGenerator.Offset;
			position /= _levelGenerator.Step;

			return position.ToIntVector();
		}
	}
}