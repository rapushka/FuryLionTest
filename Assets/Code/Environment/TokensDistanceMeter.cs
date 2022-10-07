using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class TokensDistanceMeter
	{
		private readonly Field _field;

		[Inject] public TokensDistanceMeter(Field field) => _field = field;

		public bool IsNeighboring(Token firstToken, Token secondToken)
		{
			var first = _field.GetIndexesFor(firstToken);
			var second = _field.GetIndexesFor(secondToken);

			return IsNeighbourIndexes(first, second);
		}

		private static bool IsNeighbourIndexes(Vector2Int first, Vector2Int second)
			=> IsNeighbourAt(first.x, second.x) && IsNeighbourAt(first.y, second.y);

		private static bool IsNeighbourAt(int first, int second) => Mathf.Abs(first - second) >= 1;
	}
}