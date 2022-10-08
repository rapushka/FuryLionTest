using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Settings
{
	public class SettingsWindow : MonoBehaviour
	{
		[SerializeField] private Button _buttonOK;

		private void OnEnable()
		{
			_buttonOK.onClick.AddListener(CloseWindow);
		}

		private void OnDisable()
		{
			_buttonOK.onClick.RemoveListener(CloseWindow);
		}

		public void OpenWindow() => gameObject.SetActive(true);

		private void CloseWindow() => gameObject.SetActive(false);
	}
}