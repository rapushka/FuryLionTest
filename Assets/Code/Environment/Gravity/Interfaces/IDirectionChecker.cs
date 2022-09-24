using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Interfaces
{
	public interface IDirectionChecker
	{
		bool HasPrecedentTokens(Token[,] tokens, out IEnumerable<Vector2Int> result, out Vector3 direction);
	}
}