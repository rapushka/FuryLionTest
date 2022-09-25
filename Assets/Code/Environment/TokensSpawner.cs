using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class TokensSpawner : MonoBehaviour
	{
		private Dictionary<TokenType, Token> _tokensDictionary;

		public void Construct(Dictionary<TokenType, Token> tokensDictionary)
		{
			_tokensDictionary = tokensDictionary;
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
			var token = Random.Range(0, 10) % 2 == 0
				? _tokensDictionary[TokenType.Blue]
				: _tokensDictionary[TokenType.Red];

			Instantiate(token, new Vector3(x + 0.5f, y + 0.5f), Quaternion.identity);
			tokens[x, y] = token;
		}
	}
}