using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Environment.Bonuses
{
	[CreateAssetMenu(menuName = "ScriptableObjects/TokensSpriteSheet", fileName = "TokensSpriteSheet")]
	public class TokensSpriteSheet : ScriptableObject
	{
		[SerializeField] private List<TokenSpritesEntry> _spritesEntries;

		private Dictionary<TokenUnit, TokenSpritesEntry> _dictionary;

		private void OnEnable() => _dictionary = _spritesEntries.ToDictionary((e) => e.Color, (e) => e);

		public Sprite this[Token token]
			=> token.BonusType switch
			{
				BonusType.Bomb             => _dictionary[token.TokenUnit].Bomb,
				BonusType.HorizontalRocket => _dictionary[token.TokenUnit].HorizontalRocket,
				BonusType.None             => _dictionary[token.TokenUnit].None,
				_                          => throw new ArgumentException()
			};
	}
}