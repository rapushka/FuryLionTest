using UnityEngine;

namespace Code.Gameplay
{
	public class Token : MonoBehaviour
	{
		[SerializeField] private TokenType _tokenType;

		public TokenType TokenType => _tokenType;
	}
}