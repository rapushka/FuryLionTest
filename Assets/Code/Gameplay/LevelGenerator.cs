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

		private static Vector2 FieldSize => new(GameFieldSize.Height, 0);
		
		public Token[,] Generate()
		{
			_tokenGameObjects = new Token[GameFieldSize.Height, GameFieldSize.Width];

			_tokenTypes = GetTokenTypes();
			_tokenTypes.DoubleFor(InstantiateTokenAt);

			return _tokenGameObjects;
		}

		private TokenType[,] GetTokenTypes() => _debugLevel.GetArray();

		private void InstantiateTokenAt(TokenType item, int i, int j)
			=> item.Do((t) => InstantiateInRoot(TokenOfCurrentType(t), ScaledPosition(i, j)), @if: IsForCreation);

		private void InstantiateInRoot<T>(T original, Vector3 position)
			where T : Object
			=> Instantiate(original, position, Quaternion.identity, _levelRoot);

		private Token TokenOfCurrentType(TokenType currentType) => _tokens.First((t) => t.TokenType == currentType);

		private Vector3 ScaledPosition(int x, int y) => (new Vector2(y, x) - FieldSize) * _step + _offset;

		private static bool IsForCreation(TokenType tokenType) => tokenType != TokenType.Empty;
	}
}