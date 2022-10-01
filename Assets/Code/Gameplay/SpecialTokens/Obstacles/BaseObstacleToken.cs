using System.Collections.Generic;
using System.Linq;
using Code.Environment;
using Code.Gameplay.Tokens;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.SpecialTokens.Obstacles
{
	public abstract class BaseObstacleToken : MonoBehaviour
	{
		[SerializeField] private Token _token;
		private TokensDistanceMeter _distanceMeter;
		
		private Vector2 _position;

		protected Token Token => _token;
		
		[Inject]
		public void Construct(TokensDistanceMeter distanceMeter)
		{
			_distanceMeter = distanceMeter;
		}
		
		public void OnChainComposed(LinkedList<Vector2> chain)
		{
			if (chain.Any(IsNeighbour))
			{
				ApplyMatchInNeighbour();
			}
		}

		private bool IsNeighbour(Vector2 x) => _distanceMeter.IsNeighboring(transform.position, x);

		protected abstract void ApplyMatchInNeighbour();
	}
}