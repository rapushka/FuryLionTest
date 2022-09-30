using System.Collections.Generic;
using Code.Infrastructure;
using Code.Infrastructure.Configurations;
using UnityEngine;
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

		public void OnChainEnded(LinkedList<Vector2> chain)
		{
			if (chain.Count >= _minTokensCountForChain)
			{
				_signalBus.Fire(new ChainComposedSignal(chain));
			}
		}
	}
}