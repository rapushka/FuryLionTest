using UnityEngine;

namespace Code.Gameplay
{
	public class Token : MonoBehaviour
	{
		[SerializeField] private TokenType _tokenType;
		[SerializeField] private bool _applyGravity;

		public TokenType TokenType => _tokenType;
		public bool ApplyGravity => _applyGravity;
	}
}