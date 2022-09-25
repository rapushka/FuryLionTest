using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment.Gravity.Checkers
{
	public abstract class BaseDirectionChecker : IDirectionChecker
	{
		public bool HasPrecedentTokens(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result)
		{
			throw new System.NotImplementedException();
		}
	}
}