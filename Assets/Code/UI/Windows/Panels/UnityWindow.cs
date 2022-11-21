using System;
using Code.UI.Windows.Service;
using UnityEngine;

namespace Code.UI.Windows.Panels
{
	public abstract class UnityWindow : MonoBehaviour
	{
		[SerializeField] private GameObject _window;

		public WindowResult Result { get; protected set; }
		
		public Action<WindowResult> OnClose { get; set; }
		
		public virtual void Open() => _window.SetActive(true);
		public virtual void Hide() => _window.SetActive(false);
	}
}