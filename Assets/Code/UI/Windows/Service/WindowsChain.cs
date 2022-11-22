using System;
using System.Collections.Generic;
using System.Linq;
using Code.UI.Windows.Panels;
using UnityEngine;

namespace Code.UI.Windows.Service
{
	public class WindowsChain : MonoBehaviour
	{
		[SerializeField] private List<UnityWindow> _windows;

		private Dictionary<Type, UnityWindow> _windowsDictionary;
		private Stack<WindowContainer> _windowsStack;

		private bool HasOpenedWindow => _windowsStack.Any();

		private void OnEnable()
		{
			_windowsDictionary = _windows.ToDictionary((w) => w.GetType());
			_windowsStack = new Stack<WindowContainer>();
		}

		public void Open<TWindow>(Action<TWindow> onOpen = null, Action<WindowResult> onClose = null)
			where TWindow : UnityWindow
		{
			var window = GetWindowOfType<TWindow>();

			var container = new WindowContainer(window, (w) => onOpen?.Invoke((TWindow)w), onClose);
			HideOpenedWindow();

			_windowsStack.Push(container);
			container.Open();
		}

		public void Close()
		{
			var container = _windowsStack.Pop();
			container.Close();

			if (HasOpenedWindow)
			{
				_windowsStack.Peek().Open();
			}
		}

		private UnityWindow GetWindowOfType<TWindow>()
			where TWindow : UnityWindow
		{
			if (_windowsDictionary.TryGetValue(typeof(TWindow), out var window) == false)
			{
				throw new Exception($"Window {typeof(TWindow).Name} is not found");
			}

			return window;
		}

		private void HideOpenedWindow()
		{
			if (HasOpenedWindow)
			{
				_windowsStack.Peek().Hide();
			}
		}
	}
}