using UnityEngine;

namespace Code.Token
{
	public class Token : MonoBehaviour
	{
		[SerializeField] private TokenType _tokenType;

		public TokenType TokenType => _tokenType;
	}
}