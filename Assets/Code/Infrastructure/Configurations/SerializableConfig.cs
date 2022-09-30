using System;
using UnityEngine;

namespace Code.Infrastructure.Configurations
{
	public class SerializableConfig : MonoBehaviour, IConfig
	{
		[Space] [SerializeField] private FieldConfig _fieldConfig;
		[Space] [SerializeField] private ChainConfig _chainConfig;
		[Space] [SerializeField] private InputConfig _inputConfig;
		[Space] [SerializeField] private ScoreConfig _scoreConfig;

		int IChainConfig.MinTokensCountForChain => _chainConfig.MinTokensCountForChain;

		float IInputConfig.CursorOverlapRadius => _inputConfig.CursorOverlapRadius;

		float IFieldConfig.Step => _fieldConfig.Step;

		Vector2 IFieldConfig.Offset => _fieldConfig.Offset;

		int IScoreConfig.ScoreMultiplier => _scoreConfig.ScoreMultiplier;

		float IScoreConfig.MultiplierPerTokenInChain => _scoreConfig.MultiplierPerTokenInChain;

		[Serializable]
		private class ChainConfig
		{
			public int MinTokensCountForChain = 3;
		}

		[Serializable]
		private class InputConfig
		{
			public float CursorOverlapRadius = 0.01f;
		}

		[Serializable]
		private class FieldConfig
		{
			public float Step = 1;
			public Vector2 Offset = new(0.5f, 0.5f);
		}

		[Serializable]
		private class ScoreConfig
		{
			public int ScoreMultiplier = 10;
			public float MultiplierPerTokenInChain = 2;
		}
	}
}