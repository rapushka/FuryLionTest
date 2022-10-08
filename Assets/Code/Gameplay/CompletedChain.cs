using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Tokens;
using Code.Infrastructure.Configurations.Interfaces;
using Code.Infrastructure.Signals.ActionsLeftSignals;
using Code.Infrastructure.Signals.Chain;
using Zenject;

namespace Code.Gameplay
{
	public class CompletedChain
	{
		private readonly SignalBus _signalBus;
		private readonly int _minTokensCountForChain;

		[Inject]
		public CompletedChain(SignalBus signalBus, IChainConfig chainParameters)
		{
			_signalBus = signalBus;
			_minTokensCountForChain = chainParameters.MinTokensCountForChain;
		}

		public void OnChainEnded(IEnumerable<Token> chain)
		{
			var array = chain as Token[] ?? chain.ToArray();
			if (array.Length >= _minTokensCountForChain)
			{
				_signalBus.Fire(new ChainComposedSignal(array));
				_signalBus.Fire<ActionDoneSignal>();
			}
		}
	}
}