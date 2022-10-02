using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.Configurations.SerializedImplementation
{
	public class SerializedConfig : MonoBehaviour, IConfig
	{
		[Space] [SerializeField] private SerializedFieldConfig _serializedFieldConfig;
		[Space] [SerializeField] private SerializedChainConfig _serializedChainConfig;
		[Space] [SerializeField] private SerializedInputConfig _serializedInputConfig;
		[Space] [SerializeField] private SerializedScoreConfig _serializedScoreConfig;

		int IChainConfig.MinTokensCountForChain => _serializedChainConfig.MinTokensCountForChain;

		float IInputConfig.CursorOverlapRadius => _serializedInputConfig.CursorOverlapRadius;

		float IFieldConfig.Step => _serializedFieldConfig.Step;

		Vector2 IFieldConfig.Offset => _serializedFieldConfig.Offset;

		public Vector2Int FieldSizes => _serializedFieldConfig.FieldSizes;

		int IScoreConfig.ScoreMultiplier => _serializedScoreConfig.ScoreMultiplier;

		float IScoreConfig.MultiplierPerTokenInChain => _serializedScoreConfig.MultiplierPerTokenInChain;
	}
}