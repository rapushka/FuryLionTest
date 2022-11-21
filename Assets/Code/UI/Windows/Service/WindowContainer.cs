using System;
using Code.UI.Windows.Panels;

namespace Code.UI.Windows.Service
{
	public class WindowContainer
	{
		private readonly UnityWindow _window;
		private readonly Action<WindowResult> _onClose;
		private readonly Action<UnityWindow> _onOpen;

		public WindowContainer(UnityWindow window, Action<UnityWindow> onOpen, Action<WindowResult> onClose)
		{
			_window = window;
			_onOpen = onOpen;
			_onClose = onClose;
		}

		public void Open()
		{
			_onOpen?.Invoke(_window);
			_window.Open();
		}

		public void Close()
		{
			_onClose?.Invoke(_window.Result);
			_window.Close();
		}
	}
}