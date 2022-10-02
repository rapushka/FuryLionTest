using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class ObstacleDestroyer
	{
		private readonly List<Vector2> _offsets;

		private Field _field;

		[Inject]
		public ObstacleDestroyer()
		{
			_offsets = new List<Vector2>
			{
				Vector2.up,
				Vector2.down,
				Vector2.left,
				Vector2.right,
			};
		}

		public void CheckNeighbourTokens(Token[,] tokens, Vector2 position, Field field)
		{
			_field = field;

			GetOffsetDirections(position)
				.Where((d) => IsInBounces(d, tokens))
				.Select((d) => field[d])
				.Where(IsNotEmpty)
				.ForEach(HandleObstacle);
		}

		private IEnumerable<Vector2> GetOffsetDirections(Vector2 position)
			=> _offsets.Select((offset) => offset + position);

		private static bool IsInBounces(Vector2 position, Token[,] tokens)
			=> IsInBounce(position.x, 0, tokens.GetLength(0))
			   && IsInBounce(position.y, 0, tokens.GetLength(1));

		private static bool IsNotEmpty(Token token) => token == true;

		private void HandleObstacle(Token token)
		{
			var unit = token.TokenUnit;
			if (unit is TokenUnit.Ice or TokenUnit.RockLevel1)
			{
				_field.DestroyTokenAt(token.transform.position);
			}
			else if (unit is TokenUnit.RockLevel2)
			{
				// переключить на RockLevel1
			}
		}

		private static bool IsInBounce(float n, int minValue, int maxValue) => n >= minValue && n < maxValue;
	}
}