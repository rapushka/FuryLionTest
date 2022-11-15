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

		private int _currentCoinsCount;

		[Inject]
		public CoinsCounter(ICoinsConfig coinsConfig, SignalBus signalBus)
		{
			_coinsPerToken = coinsConfig.CoinsPerToken;
			_signalBus = signalBus;
		}

		private int CurrentCoinsCount
		{
			get => _currentCoinsCount;
			set
			{
				_currentCoinsCount = value;
				InvokeValueUpdate();
			}
		}

		public void Initialize() => InvokeValueUpdate();

		public void OnChainComposed(IEnumerable<Token> chain) => IncreaseCoinsCount(chain.Count());

		public void OnTokensDestroyed(int count) => IncreaseCoinsCount(count);

		private void IncreaseCoinsCount(int count) => CurrentCoinsCount += count * _coinsPerToken;

		private void InvokeValueUpdate() => _signalBus.Fire(new CoinsCountUpdateSignal(CurrentCoinsCount));
	}
}