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

		public event Action<WindowResult> WindowClose;
		
		private bool HasOpenedWindow => _windowsStack.Any();

		private void OnEnable()
		{
			_windowsDictionary = _windows.ToDictionary((w) => w.GetType());
			_windowsStack = new Stack<UnityWindow>();
		}

		public void Open<TWindow>(Action<TWindow> initializeWindow = null)
			where TWindow : UnityWindow
		{
			var window = GetWindowOfType<TWindow>();
			HideOpenedWindow();

			_windowsStack.Push(window);
			initializeWindow?.Invoke((TWindow)window);
			window.Open();
		}

		public void Close()
		{
			var window = _windowsStack.Pop();
			window.Hide();
			WindowClose?.Invoke(window.Result);
			ClearWindowCloseSubscribers();

			if (HasOpenedWindow)
			{
				_windowsStack.Peek().Open();
			}
		}

		private void ClearWindowCloseSubscribers()
		{
			var subscribers = WindowClose?.GetInvocationList()
				?? Array.Empty<Delegate>();
			foreach (var @delegate in subscribers)
			{
				WindowClose -= (Action<WindowResult>)@delegate;
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