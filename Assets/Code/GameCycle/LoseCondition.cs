using Code.Infrastructure;
using Code.Levels;
using Zenject;

namespace Code.GameCycle
{
	public class LoseCondition
	{
		private readonly SignalBus _signalBus;
		private int _actionsRemain;

		[Inject]
		public LoseCondition(Level level, SignalBus signalBus)
		{
			_signalBus = signalBus;
			_actionsRemain = level.ActionCount;
		}

		public void OnChainComposed()
		{
			_actionsRemain--;

			if (_actionsRemain <= 0)
			{
				_signalBus.Fire<LevelLostSignal>();
			}
		}
	}
}