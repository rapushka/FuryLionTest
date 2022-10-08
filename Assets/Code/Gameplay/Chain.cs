using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Gameplay.TokensField;
using Code.Infrastructure.Signals.Chain;
using Zenject;

namespace Code.Gameplay
{
	public class Chain
	{
		private readonly SignalBus _signalBus;
		private readonly TokensDistanceMeter _distanceMeter;
		private readonly LinkedList<Token> _chainedTokens;

		private bool _chainComposingInProcess;

		[Inject]
		public Chain(SignalBus signalBus, TokensDistanceMeter distanceMeter)
		{
			_signalBus = signalBus;
			_distanceMeter = distanceMeter;

			_chainedTokens = new LinkedList<Token>();
		}

		private Token ChainHead => _chainedTokens.First();

		public void StartComposing(Token position)
		{
			var isNotStartedYet = _chainComposingInProcess == false;
			_chainComposingInProcess = true;
			position.Do(AddTokenAt, @if: isNotStartedYet);
		}

		public void NextToken(Token nextToken)
		{
			if (IsPenultimateToken(nextToken))
			{
				RemoveLastToken();
			}
			else
			{
				TryAddTokenAt(nextToken);
			}
		}

		public void EndComposing()
		{
			var isNotEndedYet = _chainComposingInProcess;
			_chainComposingInProcess = false;

			_signalBus.Fire(new ChainEndedSignal(_chainedTokens));

			_chainedTokens.Do((tokens) => tokens.Clear(), @if: isNotEndedYet);
		}

		private void TryAddTokenAt(Token token) => token.Do(AddTokenAt, @if: TokenShouldBeAdded);

		private bool IsPenultimateToken(Token nextToken)
			=> nextToken == _chainedTokens.Last?.Previous?.Value;

		private void RemoveLastToken()
		{
			_chainedTokens.RemoveLast();
			_signalBus.Fire<ChainLastTokenRemovedSignal>();
		}

		private bool TokenShouldBeAdded(Token token)
			=> _chainComposingInProcess
			   && TokenNotYetAdded(token)
			   && TokenIsFittingType(token)
			   && IsNeighborForLastToken(token);

		private void AddTokenAt(Token token)
		{
			AddTokenToChain(token);
			TokenAddedInvoke(token);
		}

		private void AddTokenToChain(Token token) => _chainedTokens.AddLast(token);

		private void TokenAddedInvoke(Token token) => _signalBus.Fire(new ChainTokenAddedSignal(token));

		private bool TokenNotYetAdded(Token token)
			=> _chainedTokens.Contains(token) == false;

		private bool TokenIsFittingType(Token token)
			=> ChainHead.TokenUnit == token.TokenUnit;

		private bool IsNeighborForLastToken(Token token)
			=> _distanceMeter.IsNeighboring(_chainedTokens.Last(), token);
	}
}