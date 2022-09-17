using System;
using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Services
{
	public class InputService : MonoBehaviour
	{
		public event Action MouseDown;
		public event Action MouseUp;

		private void Update()
		{
			this.Do((_) => MouseDown?.Invoke(), @if: Input.GetMouseButtonDown(0));
			this.Do((_) => MouseUp?.Invoke(), @if: Input.GetMouseButtonUp(0));
		}
	}
}