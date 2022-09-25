using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Interfaces
{
	public interface IDirectionMover
	{
		Token[,] Move(Token[,] tokens, Dictionary<Vector2Int, Vector3> positions);
	}
}