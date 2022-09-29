using System.Collections.Generic;
using Code.Common;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Infrastructure;
using Code.Infrastructure.IdComponents;
using Code.Levels;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Gameplay
{
	public class LevelGenerator
	{
		private readonly TokensRoot _tokensRoot;
		private readonly Vector2 _offset;
		private readonly float _step;
		private readonly Dictionary<TokenType, Token> _tokens;
		private readonly Level _level;

		private TokenType[,] _tokenTypes;
		private Token[,] _tokenGameObjects;

		[Inject]
		public LevelGenerator
			(Dictionary<TokenType, Token> tokens, Level level, Configuration configuration, TokensRoot tokensRoot)
		{
			_tokens = tokens;
			_level = level;
			_step = configuration.Field.Step;
			_offset = configuration.Field.Offset;
			_tokensRoot = tokensRoot;
		}

		public Token[,] Generate()
		{
			_tokenTypes = _level.GetArray();

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
			=> InstantiateInRoot(TokenByType(tokenType), ScalePosition(i, j));

		private T InstantiateInRoot<T>(T original, Vector3 position)
			where T : Object
			=> Object.Instantiate(original, position, Quaternion.identity, _tokensRoot.transform);

		private Token TokenByType(TokenType currentType) => _tokens[currentType];

		private Vector3 ScalePosition(int x, int y)
			=> IndexesToWorldPosition(x, y) * _step + _offset;

		private static Vector2 IndexesToWorldPosition(int x, int y)
			=> new(y, Mathf.Abs(x - (Constants.GameFieldSize.Height - 1)));

		private static bool IsForCreation(TokenType tokenType) => tokenType != TokenType.Empty;
	}
}