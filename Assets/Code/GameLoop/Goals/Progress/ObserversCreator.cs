using System;
using System.Collections.Generic;
using Code.GameLoop.Goals.Conditions;
using Code.GameLoop.Goals.Progress.ProgressObservers;
using Code.Gameplay.TokensField;
using Zenject;

namespace Code.GameLoop.Goals.Progress
{
	public class ObserversCreator
	{
		private readonly Field _field;

		[Inject]
		public ObserversCreator(Field field)
		{
			_field = field;
		}

		public List<ProgressObserver> CreateObserversListFor(IEnumerable<Goal> goals)
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