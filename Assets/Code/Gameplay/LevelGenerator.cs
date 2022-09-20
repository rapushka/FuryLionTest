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
		[SerializeField] private float _step;
		[SerializeField] private Vector2 _offset;
		[SerializeField] private Token[] _tokens;
		[SerializeField] private Transform _levelRoot;

		private Token[,] _tokenGameObjects;
		private TokenType[,] _tokenTypes;

		public Token[,] Generate()
		{
			_tokenTypes = GetTokenTypes();
			
			_tokenGameObjects = new Token[_tokenTypes.GetLength(0), _tokenTypes.GetLength(1)];
			_tokenTypes.DoubleFor(InstantiateTokenAt);

			return _tokenGameObjects;
		}

		private TokenType[,] GetTokenTypes() => _debugLevel.GetArray();

		private void InstantiateTokenAt(TokenType item, int i, int j)
			=> item.Do((tt) => CreateToArray(tt, i, j), @if: IsForCreation);

		private void CreateToArray(TokenType tokenType, int i, int j) 
			=> _tokenGameObjects[i, j] = InstantiateInRoot(TokenByType(tokenType), ScaledPosition(i, j));

		private T InstantiateInRoot<T>(T original, Vector3 position)
			where T : Object
			=> Instantiate(original, position, Quaternion.identity, _levelRoot);

		private Token TokenByType(TokenType currentType) => _tokens.First((t) => t.TokenType == currentType);

		private Vector3 ScaledPosition(int x, int y)
			=> new Vector2(y, Mathf.Abs(x - (GameFieldSize.Height - 1))) * _step + _offset;

		private static bool IsForCreation(TokenType tokenType) => tokenType != TokenType.Empty;
	}
}