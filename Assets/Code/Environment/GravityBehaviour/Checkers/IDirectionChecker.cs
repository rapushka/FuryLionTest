using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.GravityBehaviour.Checkers
{
	public interface IDirectionChecker
	{
		bool HasPrecedentTokens(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result);
	}
}