using Code.Infrastructure;
using Zenject;

namespace Code.GameLoop.Goals.Progress
{
	public class GameCycle
	{
		private readonly SignalBus _signalBus;

		private bool _isPlaying = true;

		[Inject] public GameCycle(SignalBus signalBus) => _signalBus = signalBus;

		public void OnAllGoalsReached() => EndGameAs<GameVictorySignal>();

		public void OnActionsOver() => EndGameAs<GameLoseSignal>();

		private void EndGameAs<T>()
		{
			if (_isPlaying == false)
			{
				return;
			}

			_isPlaying = false;
			_signalBus.Fire<T>();
		}
	}
}