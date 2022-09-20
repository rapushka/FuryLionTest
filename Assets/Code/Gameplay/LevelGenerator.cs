using System.Linq;
using Code.Extensions;
using Code.Levels;
using UnityEngine;
using static Code.Common.Constants;

namespace Code.Gameplay
{
	public class LevelGenerator : MonoBehaviour
	{
		[SerializeField] private Level _debugLevel;
		[SerializeField] private int _step;
		[SerializeField] private Token[] _tokens;
		
		private Token[,] _tokenGameObjects;
		private TokenType[,] _tokenTypes;

		public Token[,] Generate()
		{
			_tokenGameObjects = new Token[GameFieldSize.Height, GameFieldSize.Width];

			_tokenTypes = GetTokenTypes();
			_tokenTypes.DoubleFor(InstantiateTokenAt);

			return _tokenGameObjects;
		}

		private TokenType[,] GetTokenTypes() => _debugLevel.GetArray();

		private void InstantiateTokenAt(TokenType item, int i, int j) 
			=> item.Do((t) => Instantiate(TokenOfCurrentType(t), ScaledPosition(j, i)), @if: IsForCreation);

		private static void Instantiate<T>(T original, Vector3 position)
			where T : Object
			=> Instantiate(original, position, Quaternion.identity);

		private Token TokenOfCurrentType(TokenType currentType) => _tokens.First((t) => t.TokenType == currentType);

		private Vector3 ScaledPosition(int x, int y) => new Vector3(x, y) * _step;

		private static bool IsForCreation(TokenType tokenType) => tokenType != TokenType.Empty;
	}
}