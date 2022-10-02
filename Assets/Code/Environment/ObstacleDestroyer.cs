using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class ObstacleDestroyer
	{
		[Inject] public ObstacleDestroyer() { }

		public void CheckNeighbourTokens(Token[,] tokens, Vector2 position, Field field)
		{
			var directions = new List<Vector2>
			{
				position + Vector2.up,
				position + Vector2.down,
				position + Vector2.left,
				position + Vector2.right,
			};

			foreach (var direction in directions.Where((d) => IsNotOutOfBounce(d, tokens)))
			{
				var token = field[direction];

				if (token == true
				    && token.TokenUnit is TokenUnit.Ice or TokenUnit.RockLevel1)
				{
					field.DestroyTokenAt(token.transform.position);
				}
			}
		}

		private static bool IsNotOutOfBounce(Vector2 direction, Token[,] tokens)
			=> (IsOutOfBounce(direction.x, 0, tokens.GetLength(0))
			    || IsOutOfBounce(direction.y, 0, tokens.GetLength(1))) == false;

		private static bool IsEqual(float left, float right) => Math.Abs(left - right) < 0.01f;

		private static bool IsOutOfBounce(float n, int minValue, int maxValue) => n < minValue || n >= maxValue;
	}
}