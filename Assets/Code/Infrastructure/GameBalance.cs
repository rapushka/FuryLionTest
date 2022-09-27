using System;
using UnityEngine;

namespace Code.Infrastructure
{
	public class GameBalance : MonoBehaviour
	{
		[SerializeField] private FieldParameters _fieldParameters;

		public FieldParameters Field => _fieldParameters;
		
		[Serializable]
		public class FieldParameters
		{
			[SerializeField] private float _step;
			[SerializeField] private Vector2 _offset;

			public float Step => _step;
			public Vector2 Offset => _offset;
		}
	}
}