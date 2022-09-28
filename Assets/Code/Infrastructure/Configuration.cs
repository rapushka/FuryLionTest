using System;
using UnityEngine;

namespace Code.Infrastructure
{
	public class Configuration : MonoBehaviour
	{
		[SerializeField] private FieldParameters _fieldParameters;
		[SerializeField] private ChainParameters _chainParameters;
		[SerializeField] private InputSettings _inputSettings;

		public FieldParameters Field => _fieldParameters;

		public InputSettings Input => _inputSettings;

		public ChainParameters Chain => _chainParameters;

		[Serializable]
		public class ChainParameters
		{
			[SerializeField] private int _minTokensCountForChain = 3;

			public int MinTokensCountForChain => _minTokensCountForChain;
		}

		[Serializable]
		public class InputSettings
		{
			[SerializeField] private float _cursorOverlapRadius = 0.01f;

			public float CursorOverlapRadius => _cursorOverlapRadius;
		}

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