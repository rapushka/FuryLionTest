namespace Code.Infrastructure.Configurations
{
	public interface IScoreConfig
	{
		public int ScoreMultiplier { get; }

		public float MultiplierPerTokenInChain { get; }
	}
}