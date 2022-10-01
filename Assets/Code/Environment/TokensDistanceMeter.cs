using Code.Extensions;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Environment
{
	public class TokensDistanceMeter
	{
		private readonly float _step;

		[Inject]
		public TokensDistanceMeter(IFieldConfig fieldParameters)
		{
			_step = fieldParameters.Step;
		}

		public bool IsNeighboring(Vector2 firstPosition, Vector2 secondPosition)
			=> firstPosition.DistanceTo(secondPosition).AsAbs().LessThanOrEqualTo(_step);
	}
}