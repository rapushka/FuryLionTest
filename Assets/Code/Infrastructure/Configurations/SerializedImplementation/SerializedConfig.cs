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
		[Space] [SerializeField] private SerializedBonusesConfig _serializedBonusesConfig;
		[Space] [SerializeField] private SerializedCoinsConfig _coins;

		int IChainConfig.MinTokensCountForChain => _serializedChainConfig.MinTokensCountForChain;

		float IInputConfig.CursorOverlapRadius => _serializedInputConfig.CursorOverlapRadius;

		float IFieldConfig.Step => _serializedFieldConfig.Step;

		Vector2 IFieldConfig.Offset => _serializedFieldConfig.Offset;

		Vector2Int IFieldConfig.FieldSizes => _serializedFieldConfig.FieldSizes;

		int IScoreConfig.ScoreMultiplier => _serializedScoreConfig.ScoreMultiplier;

		float IScoreConfig.MultiplierPerTokenInChain => _serializedScoreConfig.MultiplierPerTokenInChain;

		int IBonusesConfig.MinChainLenghtForRocket => _serializedBonusesConfig.MinChainLenghtForRocket;

		int IBonusesConfig.MaxChainLenghtForRocket => _serializedBonusesConfig.MaxChainLenghtForRocket;

		int IBonusesConfig.MinChainLenghtForBomb => _serializedBonusesConfig.MinChainLenghtForBomb;

		int IBonusesConfig.BombExplosionRange => _serializedBonusesConfig.BombExplosionRange;

		int ICoinsConfig.CoinsPerToken         => _coins.CoinsPerToken;

		int ICoinsConfig.HorizontalRocketPrice => _coins.HorizontalRocketPrice;

		int ICoinsConfig.BombPrice => _coins.BombPrice;

		int ICoinsConfig.AdditionalActionPrice => _coins.AdditionalActionPrice;

		int ICoinsConfig.ActionsPerPurchase => _coins.ActionsPerPurchase;
	}
}