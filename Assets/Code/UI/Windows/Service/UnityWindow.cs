using UnityEngine;

namespace Code.UI.Windows.Service
{
	public abstract class UnityWindow : MonoBehaviour
	{
		[SerializeField] private GameObject _window;

		public WindowResult Result { get; protected set; }
		public virtual void Open() => _window.SetActive(true);
		public virtual void Hide() => _window.SetActive(false);
	}
}