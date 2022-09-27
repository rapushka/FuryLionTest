using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.GravityBehaviour.Movers
{
	public interface IDirectionMover
	{
		Token[,] Move(Token[,] tokens, Dictionary<Vector2Int, Vector3> positions);
	}
}