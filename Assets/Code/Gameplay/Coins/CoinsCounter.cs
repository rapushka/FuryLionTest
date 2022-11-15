using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.Interfaces;
using Code.Infrastructure.Signals.Coins;
using Zenject;

namespace Code.Gameplay.Coins
{
	public class CoinsCounter : IInitializable
	{
		private readonly int _coinsPerToken;
		private readonly SignalBus _signalBus;

		private int _coinsCount;

		[Inject]
		public CoinsCounter(ICoinsConfig coinsConfig, SignalBus signalBus)
		{
			_coinsPerToken = coinsConfig.CoinsPerToken;
			_signalBus = signalBus;
		}

		private int CoinsCount
		{
			get => _coinsCount;
			set
			{
				_coinsCount = value;
				InvokeValueUpdate();
			}
		}

		public void Initialize() => InvokeValueUpdate();

		public void OnChainComposed(IEnumerable<Token> chain) => IncreaseCoinsCount(chain.Count());

		public void OnTokensDestroyed(int count) => IncreaseCoinsCount(count);

		public bool TrySpent(int price)
		{
			var enough = CoinsCount >= price;
			CoinsCount -= enough ? price : 0;
			return enough;
		}

		private void IncreaseCoinsCount(int count) => CoinsCount += count * _coinsPerToken;

		private void InvokeValueUpdate() => _signalBus.Fire(new CoinsCountUpdateSignal(CoinsCount));
	}
}