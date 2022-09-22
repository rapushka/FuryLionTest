using Code.Gameplay;
using Code.Extensions;
using UnityEngine;

namespace Code.Environment
{
	public class Field : MonoBehaviour
	{
		private LevelGenerator _levelGenerator;
		private float _step;
		private Vector2 _offset;
		private Token[,] _tokens;
		private Gravity _gravity;

		public void Construct(LevelGenerator levelGenerator, Gravity gravity) 
			=> (_levelGenerator, _gravity) = (levelGenerator, gravity);

		private void Start()
		{
			_tokens = _levelGenerator.Generate();
			_step = _levelGenerator.Step;
			_offset = _levelGenerator.Offset;
		}

		public Token this[Vector2 position] 
			=> _tokens.GetByVector(position.AsIndexes(_offset, _step, _tokens.GetLength(0)));

		public bool IsNeighboring(Vector2 firstPosition, Vector2 secondPosition) 
			=> firstPosition.DistanceTo(secondPosition).AsAbs().LessThanOrEqualTo(_step);

		public void ApplyGravity() => _tokens = _gravity.Apply(_tokens);
	}
}