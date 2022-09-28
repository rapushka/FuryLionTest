using System;
using UnityEngine;

namespace Code.Infrastructure
{
	public class GameBalance : MonoBehaviour
	{
		[SerializeField] private float _mouseOverlapRadius = 0.01f;
		[SerializeField] private int _minTokensCountForChain = 3;
		[SerializeField] private FieldParameters _fieldParameters;

		public FieldParameters Field => _fieldParameters;
		public float MouseOverlapRadius => _mouseOverlapRadius;
		public int MinTokensCountForChain => _minTokensCountForChain;

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