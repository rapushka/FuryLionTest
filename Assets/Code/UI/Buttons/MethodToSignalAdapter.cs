using Zenject;

namespace Code.UI.Buttons
{
	public class MethodToSignalAdapter<T> : UnityActionToMethodAdapter
	{
		private SignalBus _signalBus;

		[Inject] public void Construct(SignalBus signalBus) => _signalBus = signalBus;

		protected override void OnClick() => _signalBus.Fire<T>();
	}
}