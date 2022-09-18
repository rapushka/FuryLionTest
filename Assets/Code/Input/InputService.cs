using System;
using Code.Extensions;
using UnityEngine;

namespace Code.Input
{
	public class InputService : MonoBehaviour
	{
		public event Action MouseDown;
		public event Action MouseUp;

		private void Update()
		{
			this.Do((_) => MouseDown?.Invoke(), @if: UnityEngine.Input.GetMouseButtonDown(0));
			this.Do((_) => MouseUp?.Invoke(), @if: UnityEngine.Input.GetMouseButtonUp(0));
		}
	}
}