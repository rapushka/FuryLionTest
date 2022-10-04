using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Environment.GravityBehaviour.Checkers
{
	public class VerticallyChecker : BaseDirectionChecker
	{
		protected override Dictionary<Vector2Int, Vector3> FillResults(Token[,] tokens)
		{
			return tokens.Where(TokenIsPrecedent)
			             .Select(Selector)
			             .ToDictionary((p) => p, GetDirection);
		}

		private Vector2Int Selector(Token t) => t.transform.position.ToVectorInt();

		protected override Vector3 GetDirection(int x, int y) => Vector3.down;

		protected override bool TokenOnDirectionIsEmpty(int x, int y) 
			=> IsNotOnBottomBorder(y) && BellowTokenIsEmpty(x, y);

		private static bool IsNotOnBottomBorder(int y) => y > 0;
		
		private bool BellowTokenIsEmpty(int x, int y) => Tokens[x, y - 1] == false;
	}
}