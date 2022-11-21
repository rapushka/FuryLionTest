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
		private Stack<UnityWindow> _windowsStack;

		private bool HasOpenedWindow => _windowsStack.Any();

		private void OnEnable()
		{
			_windowsDictionary = _windows.ToDictionary((w) => w.GetType());
			_windowsStack = new Stack<UnityWindow>();
		}

		public void Open<TWindow>(Action<TWindow> onWindowOpen = null, Action<WindowResult> onWindowClose = null)
			where TWindow : UnityWindow
		{
			var window = GetWindowOfType<TWindow>();
			window.OnClose = onWindowClose;
			HideOpenedWindow();

			_windowsStack.Push(window);
			onWindowOpen?.Invoke((TWindow)window);
			window.Open();
		}

		public void Close()
		{
			var window = _windowsStack.Pop();
			window.Hide();

			window.OnClose?.Invoke(window.Result);
			window.OnClose = null;

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