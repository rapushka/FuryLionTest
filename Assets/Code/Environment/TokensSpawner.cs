using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class TokensSpawner
	{
		private readonly TokensPool _tokensPool;
		private readonly float _step;
		private readonly Vector2 _offset;

		[Inject]
		public TokensSpawner(IFieldConfig fieldParameters, TokensPool tokensPool)
		{
			_tokensPool = tokensPool;
			_step = fieldParameters.Step;
			_offset = fieldParameters.Offset;
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
			var token = _tokensPool.CreateTokenOfType(PickRandomColor(), ScalePosition(x, y));
			tokens[x, y] = token;
		}

		private static TokenType PickRandomColor() => (TokenType)Random.Range(1, 6);

		private Vector3 ScalePosition(int x, int y) => new Vector3(x, y) + (Vector3)_offset * _step;
	}
}