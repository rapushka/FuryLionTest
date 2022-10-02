using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class TokensSpawner
	{
		private readonly TokensFactory _tokensFactory;
		private readonly float _step;
		private readonly Vector2 _offset;

		[Inject]
		public TokensSpawner(IFieldConfig fieldParameters, TokensFactory tokensFactory)
		{
			_tokensFactory = tokensFactory;
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
			var token = _tokensFactory.CreateTokenOfType(PickRandomColor(), ScalePosition(x, y));
			tokens[x, y] = token;
		}

		private static TokenUnit PickRandomColor() => (TokenUnit)Random.Range(1, 6);

		private Vector3 ScalePosition(int x, int y) => new Vector3(x, y) + (Vector3)_offset * _step;
	}
}