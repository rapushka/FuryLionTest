using System.Collections.Generic;
using Code.Extensions;
using Code.Gameplay;
using UnityEngine;

namespace Code.Environment
{
	public class TokensSpawner : MonoBehaviour
	{
		private const float Step = 0.5f;
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
			var token = _tokensDictionary.PickRandomColor();

			var tokenInstance = Instantiate(token, new Vector3(x + Step, y + Step), Quaternion.identity);
			tokens[x, y] = tokenInstance;
		}
	}
}