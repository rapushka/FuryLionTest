using System;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.View.SpritesBehaviour
{
	[Serializable]
	public class TokenSpritesEntry
	{
		[SerializeField] private TokenUnit _color;
		[SerializeField] private Sprite _none;
		[SerializeField] private Sprite _horizontalRocket;
		[SerializeField] private Sprite _bomb;

		public TokenUnit Color => _color;

		public Sprite None => _none;

		public Sprite HorizontalRocket => _horizontalRocket;

		public Sprite Bomb => _bomb;
	}
}