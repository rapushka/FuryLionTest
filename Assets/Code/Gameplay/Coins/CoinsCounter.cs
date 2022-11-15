using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Signals.Coins;
using Zenject;

namespace Code.Gameplay.Coins
{
	public class CoinsCounter : IInitializable
	{
		private readonly SignalBus _signalBus;

		private int _currentCoinsCount;

		[Inject] public CoinsCounter(SignalBus signalBus) => _signalBus = signalBus;

		public void Initialize() => InvokeValueUpdate();

		public void OnChainComposed(IEnumerable<Token> chain) => IncreaseCoinsCount(chain.Count());

		public void OnTokensDestroyed(int count) => IncreaseCoinsCount(count);

		private void IncreaseCoinsCount(int count)
		{
			_currentCoinsCount += count; // TODO: multiply by CoinsPerToken
			InvokeValueUpdate();
		}

		private void InvokeValueUpdate() => _signalBus.Fire(new CoinsCountUpdateSignal(_currentCoinsCount));
	}
}