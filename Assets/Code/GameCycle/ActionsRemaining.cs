using Code.Extensions;
using Code.Infrastructure;
using Code.Levels;
using Zenject;

namespace Code.GameCycle
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
			LoseIfActionsOver();
		}

		private void InvokeValueUpdate() => _signalBus.Fire(new RemainingActionsUpdateSignal(_actionsRemain));

		private void LoseIfActionsOver() 
			=> _signalBus.Do((signalBus) => signalBus.Fire<LevelLostSignal>(), @if: _actionsRemain <= 0);
	}
}