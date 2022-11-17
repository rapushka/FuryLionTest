using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Buttons
{
	public abstract class UnityActionToMethodAdapter : MonoBehaviour
	{
		[SerializeField] private Button _button;
		private void OnEnable() => _button.onClick.AddListener(OnClick);

		private void OnDisable() => _button.onClick.RemoveListener(OnClick);

		protected abstract void OnClick();
	}
}