using System.Collections.Generic;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Gameplay.TokensField.GravityBehaviour.Checkers
{
	public interface IDirectionChecker
	{
		bool HasContenderTokens(Token[,] tokens, out Dictionary<Vector2Int, Vector3> result);
	}
}