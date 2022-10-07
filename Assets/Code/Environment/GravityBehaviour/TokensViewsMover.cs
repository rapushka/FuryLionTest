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
			_speed = 50f;
		}

		private float ScaledSpeed => Time.deltaTime * _speed;

		public void MoveView(Token token, Vector3 to)
		{
			_coroutines.StartCoroutine(CoroutineRealization(token, to));
		}

		private IEnumerator CoroutineRealization(Component token, Vector3 to)
		{
			for (var i = 0; i < _speed; i++)
			{
				token.transform.Translate(to * (1 / _speed));
				yield return null;

			}
			
			yield break;
		}

		private void OneFrameRealization(Component token, Vector3 to)
		{
			token.transform.Translate(to);
		}

		private void DoTweenRealization(Component token, Vector3 to)
		{
			var target = token.transform.position + to;
			token.transform.DOMove(target, 1 / _speed);
		}
	}
}