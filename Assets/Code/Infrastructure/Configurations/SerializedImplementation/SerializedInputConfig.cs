using System;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.Configurations.SerializedImplementation
{
	[Serializable]
	public class SerializedInputConfig : IInputConfig
	{
		[SerializeField] private float _cursorOverlapRadius = 0.01f;

		public float CursorOverlapRadius => _cursorOverlapRadius;
	}
}