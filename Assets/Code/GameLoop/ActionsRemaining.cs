using Code.Extensions;
using Code.Infrastructure;
using Code.Levels;
using Zenject;

namespace Code.GameLoop
{
	public class ActionsRemaining : IInitializable
	{
		private readonly SignalBus _signalBus;
		private int _actionsRemain;

		[Inject]
		public ActionsRemaining(Level level, SignalBus signalBus)
		{
			_signalBus = signalBus;
			_actionsRemain = level.ActionCount;
		}

		public void Initialize() => InvokeValueUpdate();

		public void OnChainComposed()
		{
			_actionsRemain--;
			
			InvokeValueUpdate();
			CheckActionsOver();
		}

		private void InvokeValueUpdate() => _signalBus.Fire(new RemainingActionsUpdateSignal(_actionsRemain));

		private void CheckActionsOver() 
			=> _signalBus.Do((signalBus) => signalBus.Fire<ActionsOverSignal>(), @if: _actionsRemain <= 0);
	}
}