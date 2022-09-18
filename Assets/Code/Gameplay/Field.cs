using System;
using System.Linq;
using UnityEngine;

namespace Code.Gameplay
{
	public class Field : MonoBehaviour
	{
		[SerializeField] private float _step;
		[SerializeField] private Token.Token[] _tokens;

		public Token.Token this[Vector2 position]
			=> _tokens.First((token) => (Vector2)token.transform.position == position);

		public bool IsNeighboring(Token.Token first, Token.Token second)
		{
			var deltaPosition = first.transform.position - second.transform.position;
			
			return MathF.Abs(deltaPosition.x) <= _step
			       && MathF.Abs(deltaPosition.y) <= _step;
		}
	}
}