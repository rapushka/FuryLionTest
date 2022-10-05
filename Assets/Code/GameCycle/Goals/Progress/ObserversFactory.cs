using System;
using System.Collections.Generic;
using Code.Environment;
using Code.GameCycle.Goals.Conditions;
using Code.GameCycle.Goals.Progress.ProgressObservers;
using Zenject;

namespace Code.GameCycle.Goals.Progress
{
	public class ObserversFactory
	{
		private readonly Field _field;

		[Inject]
		public ObserversFactory(Field field)
		{
			_field = field;
		}

		public List<ProgressObserver> GenerateObserversListFor(IEnumerable<Goal> goals)
		{
			var result = new List<ProgressObserver>();

			foreach (var goal in goals)
			{
				ProgressObserver observer = goal switch
				{
					ReachScoreValue rs            => new ScoreValueReachedObserver(rs),
					DestroyAllObstaclesOfType @do => new DestroyAllObstaclesOfTypeObserver(@do, _field),
					DestroyNTokensOfColor dt      => new DestroyNTokensOfColorObserver(dt),
					_                             => throw new ArgumentException()
				};
				result.Add(observer);
			}

			return result;
		}
	}
}