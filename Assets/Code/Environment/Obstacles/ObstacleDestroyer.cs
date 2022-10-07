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
		private readonly List<Token> _changedTokensOnThisAction;
		private readonly List<Vector2Int> _offsets;

		[Inject]
		public ObstacleDestroyer(Field field, IFieldConfig fieldConfig)
		{
			_field = field;
			_fieldConfig = fieldConfig;

			_changedTokensOnThisAction = new List<Token>();
			_offsets = new List<Vector2Int>
			{
				Vector2Int.up,
				Vector2Int.down,
				Vector2Int.left,
				Vector2Int.right,
			};
		}

		public void OnChainComposed(IEnumerable<Token> chain)
		{
			chain.ForEach(CheckNeighbourTokens);
			_changedTokensOnThisAction.Clear();
		}

		private void CheckNeighbourTokens(Token token)
			=> GetOffsetDirections(token)
			   .Where(IsInBounces)
			   .Select((d) => _field[d])
			   .Where((t) => IsNotEmpty(t) && IsNotChangedOnThisAction(t))
			   .ForEach(HandleObstacle);

		private bool IsNotChangedOnThisAction(Token token)
			=> _changedTokensOnThisAction.Contains(token) == false;

		private IEnumerable<Vector2Int> GetOffsetDirections(Token token)
			=> _offsets.Select((offset) => offset + _field.GetIndexesFor(token));

		private static bool IsNotEmpty(Token token) => token == true;

		private void HandleObstacle(Token token)
		{
			var unit = token.TokenUnit;
			var position = _field.GetIndexesFor(token);

			if (unit is TokenUnit.Ice or TokenUnit.RockLevel1)
			{
				_field.DestroyTokenAt(position);
			}
			else if (unit is TokenUnit.RockLevel2)
			{
				_field.SwitchTokenAt(position, TokenUnit.RockLevel1);
				_changedTokensOnThisAction.Add(token);
			}
		}

		private bool IsInBounces(Vector2Int indexes)
			=> IsInBounce(indexes.x, 0, _fieldConfig.FieldSizes.x)
			   && IsInBounce(indexes.y, 0, _fieldConfig.FieldSizes.y);

		private static bool IsInBounce(int number, int minValue, int maxValue)
			=> number >= minValue && number < maxValue;
	}
}