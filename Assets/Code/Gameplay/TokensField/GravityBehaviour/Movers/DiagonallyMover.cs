using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.TokensField.GravityBehaviour.Movers
{
	public class DiagonallyMover : BaseDirectionMover
	{
		protected override void FallTokenAtDirection(KeyValuePair<Vector2Int, Vector3> pair)
		{
			var position = pair.Key;
			var direction = pair.Value;

			MoveToken(position.x, position.y, to: direction + Vector3.down);
		}
	}
}