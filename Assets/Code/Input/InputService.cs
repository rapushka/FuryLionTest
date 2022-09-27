using System;
using Code.Extensions;
using Code.Infrastructure;
using UnityEngine;
using Zenject;

namespace Code.Input
{
	public class InputService : MonoBehaviour
	{
		private SignalBus _signalBus;

		[Inject] public void Construct(SignalBus signalBus) => _signalBus = signalBus;

		private void Update()
		{
			
			
			this.Do((_) => _signalBus.Fire<MouseDownSignal>(), @if: UnityEngine.Input.GetMouseButtonDown(0));
			this.Do((_) => _signalBus.Fire<MouseUpSignal>(), @if: UnityEngine.Input.GetMouseButtonUp(0));
		}
	}
}