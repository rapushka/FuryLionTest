using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay;
using Code.Infrastructure;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class TokensSpawner
	{
		private readonly Dictionary<TokenType, Token> _tokensDictionary;
		private readonly float _step;
		private readonly Vector2 _offset;

		[Inject]
		public TokensSpawner(Dictionary<TokenType, Token> tokensDictionary, GameBalance balance)
		{
			_tokensDictionary = tokensDictionary;
			_step = balance.Field.Step;
			_offset = balance.Field.Offset;
		}

		public bool Spawn(Token[,] tokens)
		{
			var created = false;
			var y = tokens.GetLength(1) - 1;

			for (var x = 0; x < tokens.GetLength(0); x++)
			{
				if (tokens[x, y] == true)
				{
					continue;
				}

				CreateToken(tokens, x, y);
				created = true;
			}

			return created;
		}

		private void CreateToken(Token[,] tokens, int x, int y)
		{
			var tokenPrefab = _tokensDictionary.PickRandomColor();

			var token = Object.Instantiate(tokenPrefab, ScalePosition(x, y), Quaternion.identity);
			tokens[x, y] = token;
		}

		private Vector3 ScalePosition(int x, int y) => new Vector3(x, y) + (Vector3)_offset * _step;
	}
}