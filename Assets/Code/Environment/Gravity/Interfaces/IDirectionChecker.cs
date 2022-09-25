using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Interfaces
{
	public interface IDirectionChecker
	{
		bool HasPrecedentTokens(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result);
	}
}