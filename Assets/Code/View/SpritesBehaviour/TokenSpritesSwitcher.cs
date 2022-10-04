using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.View.SpritesBehaviour
{
	public class TokenSpritesSwitcher
	{
		private readonly TokensSpriteSheet _spriteSheet;

		[Inject] public TokenSpritesSwitcher(TokensSpriteSheet spriteSheet) => _spriteSheet = spriteSheet;

		public void OnBonusSpawned(Token token) => token.GetComponent<SpriteRenderer>().sprite = _spriteSheet[token];
	}
}