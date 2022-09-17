using System;
using UnityEngine;

namespace Code.Services
{
	public class InputService : MonoBehaviour
	{
		public event Action MouseDown;
		public event Action MouseUp;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				MouseDown?.Invoke();
			}

			if (Input.GetMouseButtonUp(0))
			{
				MouseUp?.Invoke();
			}
		}
	}
}