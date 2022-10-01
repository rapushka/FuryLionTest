using Code.Common;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.Interfaces;
using Code.Levels;
using UnityEngine;
using Zenject;

namespace Code.Gameplay
{
	public class LevelGenerator
	{
		private readonly Vector2 _offset;
		private readonly float _step;
		private readonly Level _level;
		private readonly TokensPool _tokensPool;

		private TokenType[,] _tokenTypes;
		private Token[,] _tokenGameObjects;

		[Inject]
		public LevelGenerator(Level level, IFieldConfig fieldConfig, TokensPool tokensPool)
		{
			_level = level;
			_tokensPool = tokensPool;
			_step = fieldConfig.Step;
			_offset = fieldConfig.Offset;
		}

		public Token[,] Generate()
		{
			_tokenTypes = _level.TokenTypesArray;

			_tokenGameObjects = CreateAdaptedArray();
			_tokenTypes.DoubleFor(InstantiateTokenAt);

			return _tokenGameObjects;
		}

		private Token[,] CreateAdaptedArray() => new Token[_tokenTypes.GetLength(1), _tokenTypes.GetLength(0)];

		private void InstantiateTokenAt(TokenType item, int i, int j)
			=> item.Do((tokenType) => CreateInArray(tokenType, i, j), @if: IsForCreation);

		private void CreateInArray(TokenType tokenType, int i, int j)
			=> _tokenGameObjects.SetAtVector(IndexesToWorldPosition(i, j).ToVectorInt(), Value(tokenType, i, j));

		private Token Value(TokenType tokenType, int i, int j)
			=> _tokensPool.CreateTokenOfType(tokenType, ScalePosition(i, j));

		private Vector3 ScalePosition(int x, int y)
			=> IndexesToWorldPosition(x, y) * _step + _offset;

		private static Vector2 IndexesToWorldPosition(int x, int y)
			=> new(y, Mathf.Abs(x - (Constants.GameFieldSize.Height - 1)));

		private static bool IsForCreation(TokenType tokenType) => tokenType != TokenType.Empty;
	}
}