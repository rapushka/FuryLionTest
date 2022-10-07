using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Environment.Obstacles
{
	public class ObstacleDestroyer
	{
		private readonly Field _field;
		private readonly IFieldConfig _fieldConfig;
		private readonly List<Vector2> _changedTokensOnThisAction;
		private readonly List<Vector2> _offsets;

		[Inject]
		public ObstacleDestroyer(Field field, IFieldConfig fieldConfig)
		{
			_field = field;
			_fieldConfig = fieldConfig;

			_changedTokensOnThisAction = new List<Vector2>();
			_offsets = new List<Vector2>
			{
				Vector2.up,
				Vector2.down,
				Vector2.left,
				Vector2.right,
			};
		}

		public void OnChainComposed(IEnumerable<Vector2> chain)
		{
			chain.ForEach(CheckNeighbourTokens);
			_changedTokensOnThisAction.Clear();
		}

		private void CheckNeighbourTokens(Vector2 position)
			=> GetOffsetDirections(position)
			   .Where(IsInBounces)
			   .Select((d) => _field[d])
			   .Where((t) => IsNotEmpty(t) && IsNotChangedOnThisAction(t))
			   .ForEach(HandleObstacle);

		private bool IsNotChangedOnThisAction(Component token)
			=> _changedTokensOnThisAction.Contains(token.transform.position) == false; // TODO: !!!

		private IEnumerable<Vector2> GetOffsetDirections(Vector2 position)
			=> _offsets.Select((offset) => offset + position);

		private bool IsInBounces(Vector2 position)
			=> IsInBounce(position.x, 0, _fieldConfig.FieldSizes.x)
			   && IsInBounce(position.y, 0, _fieldConfig.FieldSizes.y);

		private static bool IsNotEmpty(Token token) => token == true;

		private void HandleObstacle(Token token)
		{
			var unit = token.TokenUnit;
			var position = token.transform.position; // TODO: !!!

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