using Code.Gameplay;
using Code.Extensions;
using UnityEngine;

namespace Code.Environment
{
	public class Field : MonoBehaviour
	{
		private LevelGenerator _levelGenerator;
		private float _step;
		private Token[,] _tokens;
		private Gravity _gravity;

		public void Construct(LevelGenerator levelGenerator, Gravity gravity) 
			=> (_levelGenerator, _gravity) = (levelGenerator, gravity);

		private void Start()
		{
			_tokens = _levelGenerator.Generate();
			_step = _levelGenerator.Step;

			// ApplyGravity();
		}

		public Token this[Vector2 position] 
			=> _tokens.GetAtVector(position.ToVectorInt());

		public bool IsNeighboring(Vector2 firstPosition, Vector2 secondPosition) 
			=> firstPosition.DistanceTo(secondPosition).AsAbs().LessThanOrEqualTo(_step);

		public void ApplyGravity() => _tokens = _gravity.Apply(_tokens);
	}
}