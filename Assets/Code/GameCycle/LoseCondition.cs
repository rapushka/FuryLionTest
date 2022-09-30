using Code.Infrastructure;
using Code.Levels;
using Zenject;

namespace Code.GameCycle
{
	public class LoseCondition
	{
		private readonly SignalBus _signalBus;
		private int _actionsLeft;

		[Inject]
		public LoseCondition(Level level, SignalBus signalBus)
		{
			_signalBus = signalBus;
			_actionsLeft = level.ActionCount;
		}

		public void OnChainComposed()
		{
			_actionsLeft--;

			if (_actionsLeft <= 0)
			{
				_signalBus.Fire<LevelLostSignal>();
			}
		}
	}
}