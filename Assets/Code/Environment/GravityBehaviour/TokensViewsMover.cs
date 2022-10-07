using System.Collections;
using Code.Gameplay.Tokens;
using Code.Inner.CustomMonoBehaviours;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Environment.GravityBehaviour
{
	public class TokensViewsMover
	{
		private readonly float _speed;
		private readonly CoroutinesHandler _coroutines;

		[Inject]
		public TokensViewsMover(CoroutinesHandler coroutines)
		{
			_coroutines = coroutines;
			_speed = 3f;
		}

		private float ScaledSpeed => Time.deltaTime * _speed;

		public void MoveView(Token token, Vector3 to)
		{
			_coroutines.StartRoutine(CoroutineRealization(token, to));
		}

		private IEnumerator CoroutineRealization(Component token, Vector3 to)
		{
			var target = token.transform.position + to;
			
			while (token.transform.position !=target)
			{
				token.transform.position = Vector3.Lerp(token.transform.position, target, ScaledSpeed);
				yield return null;
			}
		}

		private void OneFrameRealization(Component token, Vector3 to)
		{
			token.transform.Translate(to);
		}

		private void DoTweenRealization(Token token, Vector3 to)
		{
			var target = token.transform.position + to;
			token.transform.DOMove(target, 0.5f);
		}
	}
}