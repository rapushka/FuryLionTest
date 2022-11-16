using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.UI.Windows.Service
{
	public class WindowsService : MonoBehaviour
	{
		[SerializeField] private List<UnityWindow> _windows;

		private Dictionary<Type, UnityWindow> _windowsDictionary;
		private Stack<UnityWindow> _windowsStack;

		private bool HasOpenedWindow => _windowsStack.Any();

		private void OnEnable() => _windowsDictionary = _windows.ToDictionary((w) => w.GetType());

		public void OpenNew<TWindow>(Action<TWindow> initializeWindow = null)
			where TWindow : UnityWindow
		{
			var window = GetWindowOfType<TWindow>();
			HideOpenedWindow();

			_windowsStack.Push(window);
			initializeWindow?.Invoke((TWindow)window);
			window.Open();
		}

		public void Close(Action<WindowResult> onClosed = null)
		{
			var window = _windowsStack.Pop();
			window.Hide();
			onClosed?.Invoke(window.Result);

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