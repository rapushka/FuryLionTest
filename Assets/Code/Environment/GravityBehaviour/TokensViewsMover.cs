using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Environment.GravityBehaviour
{
	public class TokensViewsMover
	{
		public void MoveView(Token token, Vector3 to)
		{
			token.transform.Translate(to);
		}
	}
}