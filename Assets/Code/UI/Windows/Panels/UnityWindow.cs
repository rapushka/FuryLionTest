using System;
using Code.UI.Windows.Service;
using UnityEngine;

namespace Code.UI.Windows.Panels
{
	public abstract class UnityWindow : MonoBehaviour
	{
		[SerializeField] private GameObject _window;

		protected WindowResult Result;
		private Action<WindowResult> _onClose;

		public Action<WindowResult> OnClose
		{
			set
			{
				if (_onClose != null)
				{
					throw new InvalidOperationException("OnClose already set");
				}

				_onClose = value ?? throw new InvalidOperationException("Cannot reset to null from outside");
			}
		}

		public virtual void Open() => _window.SetActive(true);

		public virtual void Hide()
		{
			_window.SetActive(false);

			_onClose?.Invoke(Result);
			_onClose = null;
		}
	}
}