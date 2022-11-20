using Code.Infrastructure.Signals.Input;
using Zenject;
using UnityEngine.EventSystems;

namespace Code.Input
{
	public class InputService : ITickable
	{
		private readonly SignalBus _signalBus;

		[Inject] public InputService(SignalBus signalBus) => _signalBus = signalBus;

		private static bool MouseNotOverUI => EventSystem.current.IsPointerOverGameObject() == false;

		public void Tick()
		{
			if (UnityEngine.Input.GetMouseButtonDown(0) && MouseNotOverUI)
			{
				_signalBus.Fire<MouseDownSignal>();
			}

			if (UnityEngine.Input.GetMouseButtonUp(0) && MouseNotOverUI)
			{
				_signalBus.Fire<MouseUpSignal>();
			}
		}
	}
}