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
		private readonly List<Vector2> _changedTokensOnThisAction;
		private readonly List<Vector2> _offsets;

		private Field _field;

		[Inject]
		public ObstacleDestroyer()
		{
			_changedTokensOnThisAction = new List<Vector2>();

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
				.Where((t) => IsNotEmpty(t) && IsNotChangedOnThisAction(t))
				.ForEach(HandleObstacle);
		}

		public void Clear() => _changedTokensOnThisAction.Clear();

		private bool IsNotChangedOnThisAction(Component token)
			=> _changedTokensOnThisAction.Contains(token.transform.position) == false;

		private IEnumerable<Vector2> GetOffsetDirections(Vector2 position)
			=> _offsets.Select((offset) => offset + position);

		private static bool IsInBounces(Vector2 position, Token[,] tokens)
			=> IsInBounce(position.x, 0, tokens.GetLength(0))
			   && IsInBounce(position.y, 0, tokens.GetLength(1));

		private static bool IsNotEmpty(Token token) => token == true;

		private void HandleObstacle(Token token)
		{
			var unit = token.TokenUnit;
			var position = token.transform.position;

			if (unit is TokenUnit.Ice or TokenUnit.RockLevel1)
			{
				_field.DestroyTokenAt(position);
			}
			else if (unit is TokenUnit.RockLevel2)
			{
				_field.SwitchTokenAt(position, TokenUnit.RockLevel1);
				_changedTokensOnThisAction.Add(position);
			}
		}

		private static bool IsInBounce(float number, int minValue, int maxValue)
			=> number >= minValue && number < maxValue;
	}
}