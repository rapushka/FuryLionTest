using System;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.Configurations.SerializedImplementation
{
	[Serializable]
	public class SerializedFieldConfig : IFieldConfig
	{
		[SerializeField] private float _step = 1;
		[SerializeField] private Vector2 _offset = new(0.5f, 0.5f);
		
		public float Step => _step;
		public Vector2 Offset => _offset;
	}
}