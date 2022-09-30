using UnityEngine;

namespace Code.Infrastructure.Configurations
{
	public class Configuration : MonoBehaviour, IFieldConfig, IChainConfig, IInputConfig, IScoreConfig
	{
		[SerializeField] private int _minTokensCountForChain = 3;
		[SerializeField] private float _cursorOverlapRadius = 0.01f;
		[SerializeField] private float _step;
		[SerializeField] private Vector2 _offset;
		[SerializeField] private int _scoreMultiplier = 10;
		[SerializeField] private float _multiplierPerTokenInChain = 2;

		public int MinTokensCountForChain => _minTokensCountForChain;
		
		public float CursorOverlapRadius => _cursorOverlapRadius;
		
		public float Step => _step;
		
		public Vector2 Offset => _offset;

		public int ScoreMultiplier => _scoreMultiplier;

		public float MultiplierPerTokenInChain => _multiplierPerTokenInChain;
	}
}