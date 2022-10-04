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
		private readonly TokensFactory _tokensFactory;

		private TokenUnit[,] _tokenTypes;
		private Token[,] _tokenGameObjects;

		[Inject]
		public LevelGenerator(Level level, IFieldConfig fieldConfig, TokensFactory tokensFactory)
		{
			_level = level;
			_tokensFactory = tokensFactory;
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

		private void InstantiateTokenAt(TokenUnit item, int i, int j)
			=> item.Do((tokenType) => CreateInArray(tokenType, i, j), @if: IsForCreation);

		private void CreateInArray(TokenUnit tokenUnit, int i, int j)
			=> _tokenGameObjects.SetAtVector(IndexesToWorldPosition(i, j).ToVectorInt(), Value(tokenUnit, i, j));

		private Token Value(TokenUnit tokenUnit, int i, int j)
			=> _tokensFactory.CreateTokenForUnit(tokenUnit, ScalePosition(i, j));

		private Vector3 ScalePosition(int x, int y)
			=> IndexesToWorldPosition(x, y) * _step + _offset;

		private static Vector2 IndexesToWorldPosition(int x, int y)
			=> new(y, Mathf.Abs(x - (Constants.GameFieldSize.Height - 1)));

		private static bool IsForCreation(TokenUnit tokenUnit) => tokenUnit != TokenUnit.Empty;
	}
}