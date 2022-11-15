using System;
using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Signals.Bonuses;
using Zenject;

namespace Code.Gameplay.TokensField.Bonuses
{
	public class BonusSpawner
	{
		private readonly Field _field;
		private readonly SignalBus _signalBus;
		private TokenUnit _unit;
		private Token[] _chain;

		[Inject]
		public BonusSpawner(Field field, SignalBus signalBus)
		{
			_field = field;
			_signalBus = signalBus;
		}

		public void SpawnHorizontalRocket()
			=> SpawnHorizontalRocket(Array.Empty<Token>(), _field.GetRandomToken().TokenUnit);

		public void SpawnBomb()
			=> SpawnBomb(Array.Empty<Token>(), _field.GetRandomToken().TokenUnit);

		public void SpawnHorizontalRocket(Token[] chain, TokenUnit unit)
			=> TrySpawn(chain, unit, BonusType.HorizontalRocket);

		public void SpawnBomb(Token[] chain, TokenUnit unit)
			=> TrySpawn(chain, unit, BonusType.Bomb);

		private void TrySpawn(Token[] chain, TokenUnit unit, BonusType bonusType)
		{
			_chain = chain;
			_unit = unit;
			var tokens = _field.Where(CasualTokenOfRightUnit);

			// ReSharper disable PossibleMultipleEnumeration - There is no multiple enumeration
			if (tokens.Any())
			{
				Spawn(bonusType, tokens);
			}
		}

		private void Spawn(BonusType bonusType, IEnumerable<Token> tokens)
		{
			var token = tokens.PickRandom();
			token.BonusType = bonusType;
			_signalBus.Fire(new BonusSpawnedSignal(token));
		}

		private bool CasualTokenOfRightUnit(Token token)
			=> token == true
			   && token.TokenUnit == _unit
			   && token.BonusType == BonusType.None
			   && _chain.Contains(token) == false;
	}
}