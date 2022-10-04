using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure;
using Code.Infrastructure.Configurations.Interfaces;
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

		public void OnChainEnded(IEnumerable<Vector2> chain)
		{
			var array = chain as Vector2[] ?? chain.ToArray();
			if (array.Length >= _minTokensCountForChain)
			{
				_signalBus.Fire(new ChainComposedSignal(array));
			}
		}
	}
}