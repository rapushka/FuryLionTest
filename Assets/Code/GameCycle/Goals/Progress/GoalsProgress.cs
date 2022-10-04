using System;
using System.Collections.Generic;
using Code.Environment;
using Code.GameCycle.Goals.Conditions;
using Code.GameCycle.Goals.Progress.ProgressObservers;
using Code.Levels;
using Zenject;

namespace Code.GameCycle.Goals.Progress
{
	public class GoalsProgress
	{
		private readonly List<ProgressObserver> _progressObservers;
		private readonly Field _field;

		[Inject]
		public GoalsProgress(Level currentLevel, Field field)
		{
			_progressObservers = GenerateObserversFor(currentLevel.Goals);
			_field = field;
		}

		private List<ProgressObserver> GenerateObserversFor(List<Goal> goals)
		{
			var result = new List<ProgressObserver>(goals.Count);

			foreach (var goal in goals)
			{
				ProgressObserver observer = goal switch
				{
					ReachScoreValue rs           => new ScoreValueReachedObserver(rs),
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