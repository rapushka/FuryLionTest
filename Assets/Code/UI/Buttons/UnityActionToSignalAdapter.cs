using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Buttons
{
	public class UnityActionToSignalAdapter<T> : MonoBehaviour
	{
		[SerializeField] private Button _button;

		private SignalBus _signalBus;

		[Inject] public void Construct(SignalBus signalBus) => _signalBus = signalBus;

		private void OnEnable() => _button.onClick.AddListener(OnClick);

		private void OnDisable() => _button.onClick.RemoveListener(OnClick);

		private void OnClick() => _signalBus.Fire<T>();
	}
}