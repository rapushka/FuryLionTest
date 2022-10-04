namespace Code.Infrastructure.Configurations.Interfaces
{
	public interface IScoreConfig
	{
		public int ScoreMultiplier { get; }

		public float MultiplierPerTokenInChain { get; }
	}
}