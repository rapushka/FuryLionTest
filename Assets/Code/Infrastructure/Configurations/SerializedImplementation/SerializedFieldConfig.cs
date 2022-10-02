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
		[SerializeField] private Vector2Int _fieldSizes = new(7, 11);

		public float Step => _step;
		public Vector2 Offset => _offset;
		public Vector2Int FieldSizes => _fieldSizes;
	}
}