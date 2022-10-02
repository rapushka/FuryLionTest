using System;
using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class ObstacleDestroyer
	{
		[Inject] public ObstacleDestroyer() { }

		public void CheckNeighbourTokens(Token[,] tokens, Vector2 destroyedTokenPosition, Field field)
		{
			var startValue = destroyedTokenPosition - Vector2.one;
			var endValue = destroyedTokenPosition + Vector2.one;

			for (var x = (int)startValue.x; x < endValue.x; x++)
			{
				for (var y = (int)startValue.y; y < endValue.y; y++)
				{
					if (IsOutOfBounce(x, 0, tokens.GetLength(0))
					    || IsOutOfBounce(y, 0, tokens.GetLength(1)))
					{
						continue;
					}

					var token = tokens[x, y];
					
					if (token == null)
					{
						continue;
					}

					if (token.TokenUnit is TokenUnit.Ice or TokenUnit.RockLevel1)
					{
						field.DestroyTokenAt(token.transform.position);
					}
				}
			}
		}

		private static bool IsEqual(float left, float right) => Math.Abs(left - right) < 0.01f;

		private static bool IsOutOfBounce(float n, int minValue, int maxValue) => n < minValue || n >= maxValue;
	}
}