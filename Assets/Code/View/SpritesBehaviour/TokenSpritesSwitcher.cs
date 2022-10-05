using Code.Environment.Bonuses;
using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.View.SpritesBehaviour
{
	public class TokenSpritesSwitcher
	{
		private readonly TokensSpriteSheet _spriteSheet;

		[Inject] public TokenSpritesSwitcher(TokensSpriteSheet spriteSheet) => _spriteSheet = spriteSheet;

		public void OnTokenDestroyed(Token token)
		{
			if (token.BonusType == BonusType.None)
			{
				return;
			}

			token.BonusType = BonusType.None;
			UpdateTokenSprite(token);
		}

		public void OnBonusSpawned(Token token) => UpdateTokenSprite(token);

		private void UpdateTokenSprite(Token t) => t.GetComponent<SpriteRenderer>().sprite = _spriteSheet[t];
	}
}