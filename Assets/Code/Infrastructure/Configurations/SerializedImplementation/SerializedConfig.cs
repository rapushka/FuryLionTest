using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.Configurations.SerializedImplementation
{
	public class SerializedConfig : MonoBehaviour, IConfig
	{
		[Space] [SerializeField] private SerializedFieldConfig _field;
		[Space] [SerializeField] private SerializedChainConfig _chain;
		[Space] [SerializeField] private SerializedInputConfig _input;
		[Space] [SerializeField] private SerializedScoreConfig _score;
		[Space] [SerializeField] private SerializedBonusConfig _bonus;
		[Space] [SerializeField] private SerializedCoinsConfig _coins;

		int IChainConfig.MinTokensCountForChain => _chain.MinTokensCountForChain;

		float IInputConfig.CursorOverlapRadius => _input.CursorOverlapRadius;

		float IFieldConfig.Step => _field.Step;

		Vector2 IFieldConfig.Offset => _field.Offset;

		Vector2Int IFieldConfig.FieldSizes => _field.FieldSizes;

		int IScoreConfig.ScoreMultiplier => _score.ScoreMultiplier;

		float IScoreConfig.MultiplierPerTokenInChain => _score.MultiplierPerTokenInChain;

		int IBonusesConfig.MinChainLenghtForRocket => _bonus.MinChainLenghtForRocket;

		int IBonusesConfig.MaxChainLenghtForRocket => _bonus.MaxChainLenghtForRocket;

		int IBonusesConfig.MinChainLenghtForBomb => _bonus.MinChainLenghtForBomb;

		int IBonusesConfig.BombExplosionRange => _bonus.BombExplosionRange;

		int ICoinsConfig.CoinsPerToken         => _coins.CoinsPerToken;

		int ICoinsConfig.HorizontalRocketPrice => _coins.HorizontalRocketPrice;

		int ICoinsConfig.BombPrice => _coins.BombPrice;

		int ICoinsConfig.AdditionalActionsPrice => _coins.AdditionalActionsPrice;

		int ICoinsConfig.ActionsCountPerPurchase => _coins.ActionsCountPerPurchase;
	}
}