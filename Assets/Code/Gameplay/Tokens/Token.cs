using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Tokens
{
	public class Token : MonoBehaviour
	{
		[FormerlySerializedAs("_tokenType")] [SerializeField] private TokenUnit _tokenUnit;
		[SerializeField] private bool _applyGravity;

		public TokenUnit TokenUnit => _tokenUnit;
		public bool ApplyGravity => _applyGravity;
	}
}