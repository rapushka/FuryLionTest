using System.Collections.Generic;
using Code.Extensions;
using Code.Infrastructure;
using Code.Levels;
using UnityEngine;
using static Code.Common.Constants;
using Object = UnityEngine.Object;

namespace Code.Gameplay
{
	public class LevelGenerator : MonoBehaviour
	{
		[SerializeField] private Level _debugLevel;
		[SerializeField] private Transform _levelRoot;

		private Vector2 _offset;
		private float _step;
		private Dictionary<TokenType, Token> _tokens;
		private Token[,] _tokenGameObjects;
		private TokenType[,] _tokenTypes;

		public void Construct(Dictionary<TokenType, Token> tokens, GameBalance gameBalance)
		{
			_tokens = tokens;
			
			_step = gameBalance.Field.Step;
			_offset = gameBalance.Field.Offset;
		}
		
		public Token[,] Generate()
		{
			_tokenTypes = GetTokenTypes();
			
			_tokenGameObjects = new Token[_tokenTypes.GetLength(1), _tokenTypes.GetLength(0)];
			_tokenTypes.DoubleFor(InstantiateTokenAt);

			return _tokenGameObjects;
		}

		private TokenType[,] GetTokenTypes() => _debugLevel.GetArray();

		private void InstantiateTokenAt(TokenType item, int i, int j)
			=> item.Do((tt) => CreateToArray(tt, i, j), @if: IsForCreation);

		private void CreateToArray(TokenType tokenType, int i, int j) 
			=> _tokenGameObjects.SetAtVector(ToUnityWorldPosition(i, j).ToVectorInt(), Value(tokenType, i, j));

		private Token Value(TokenType tokenType, int i, int j) 
			=> InstantiateInRoot(TokenByType(tokenType), ScalePosition(i, j));

		private T InstantiateInRoot<T>(T original, Vector3 position)
			where T : Object
			=> Instantiate(original, position, Quaternion.identity, _levelRoot);

		private Token TokenByType(TokenType currentType) => _tokens[currentType];

		private Vector3 ScalePosition(int x, int y)
			=> ToUnityWorldPosition(x, y) * _step + _offset;

		private static Vector2 ToUnityWorldPosition(int x, int y) 
			=> new(y, Mathf.Abs(x - (GameFieldSize.Height - 1)));

		private static bool IsForCreation(TokenType tokenType) => tokenType != TokenType.Empty;
	}
}