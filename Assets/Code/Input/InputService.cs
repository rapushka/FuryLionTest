using Code.Infrastructure.Signals.Input;
using Zenject;

namespace Code.Input
{
	public class InputService : ITickable
	{
		private readonly SignalBus _signalBus;

		[Inject] public InputService(SignalBus signalBus) => _signalBus = signalBus;

		public void Tick()
		{
			if (UnityEngine.Input.GetMouseButtonDown(0))
			{
				_signalBus.Fire<MouseDownSignal>();
			}

			if (UnityEngine.Input.GetMouseButtonUp(0))
			{
				_signalBus.Fire<MouseUpSignal>();
			}
		}
	}
}