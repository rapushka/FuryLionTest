using System.Collections;
using Code.Gameplay.Tokens;
using Code.Inner.CustomMonoBehaviours;
using UnityEngine;
using Zenject;

namespace Code.Environment.GravityBehaviour
{
	public class TokensViewsMover
	{
		private const float Speed = 3f;
		private readonly CoroutinesHandler _coroutines;

		[Inject]
		public TokensViewsMover(CoroutinesHandler coroutines)
		{
			_coroutines = coroutines;
		}
		
		private float ScaledSpeed => Time.deltaTime * Speed;

		public void MoveView(Token token, in Vector3 to)
		{
			if (token == false)
			{
				return;
			}

			_coroutines.StartRoutine(Move(token, to));
		}

		private IEnumerator Move(Component token, Vector3 targetPosition)
		{
			token.transform.Translate(targetPosition);
			yield break;
		}
	}
}