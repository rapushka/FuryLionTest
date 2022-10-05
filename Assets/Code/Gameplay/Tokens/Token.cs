using Code.Environment.Bonuses;
using UnityEngine;

namespace Code.Gameplay.Tokens
{
	public class Token : MonoBehaviour
	{
		[SerializeField] private TokenUnit _tokenUnit;
		[SerializeField] private bool _applyGravity;
		[SerializeField] private SpriteRenderer _spriteRenderer;

		public TokenUnit TokenUnit => _tokenUnit;

		public bool ApplyGravity => _applyGravity;

		public BonusType BonusType { get; set; }

		public Sprite Sprite
		{
			set => _spriteRenderer.sprite = value;
		}
	}
}