using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Interfaces
{
	public interface IDirectionMover
	{
		Token[,] Move(Token[,] tokens, IEnumerable<Vector2Int> positions, Vector3 direction);
	}
}