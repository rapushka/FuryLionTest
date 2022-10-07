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
			token.transform.Translate(to);

			// _coroutines.StartRoutine(Move(token, to));
		}

		private IEnumerator Move(Component token, Vector3 to)
		{
			// token.transform.Translate(to);
			
			var target = Vector3.Lerp(token.transform.position, to, ScaledSpeed);
			token.transform.DOMove(target, 1f);

			yield break;
		}
	}
}