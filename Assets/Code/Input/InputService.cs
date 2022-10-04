using Code.Extensions;
using Code.Infrastructure;
using Zenject;

namespace Code.Input
{
	public class InputService : ITickable
	{
		private readonly SignalBus _signalBus;

		[Inject] public InputService(SignalBus signalBus) => _signalBus = signalBus;

		public void Tick()
			=> _signalBus.Do((s) => s.Fire<MouseDownSignal>(), @if: UnityEngine.Input.GetMouseButtonDown(0))
			             .Do((s) => s.Fire<MouseUpSignal>(), @if: UnityEngine.Input.GetMouseButtonUp(0));
	}
}