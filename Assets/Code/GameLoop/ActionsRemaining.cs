using Code.Extensions;
using Code.Infrastructure.Signals.ActionsLeftSignals;
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

		public void OnActionDone()
		{
			_actionsRemain--;

			InvokeValueUpdate();
			CheckActionsOver();
		}
		
		public void OnActionsBought(int count)
		{
			_actionsRemain += count;

			InvokeValueUpdate();
		}

		private void InvokeValueUpdate() => _signalBus.Fire(new ActionsLeftUpdateSignal(_actionsRemain));

		private void CheckActionsOver()
			=> _signalBus.Do((signalBus) => signalBus.Fire<ActionsOverSignal>(), @if: _actionsRemain <= 0);
	}
}