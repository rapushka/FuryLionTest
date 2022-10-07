using System.Collections;
using Code.Gameplay.Tokens;
using Code.Inner.CustomMonoBehaviours;
using UnityEngine;
using Zenject;

namespace Code.Environment.GravityBehaviour
{
	public class TokensViewsMover
	{
		private readonly float _framesCountForMovingToken;
		private readonly CoroutinesHandler _coroutines;

		[Inject]
		public TokensViewsMover(CoroutinesHandler coroutines)
		{
			_coroutines = coroutines;
			_framesCountForMovingToken = 50f;
		}

		public void MoveView(Token token, Vector3 to)
		{
			_coroutines.StartCoroutine(CoroutineRealization(token, to));
		}

		private IEnumerator CoroutineRealization(Component token, Vector3 to)
		{
			for (var i = 0; i < _framesCountForMovingToken; i++)
			{
				token.transform.Translate(to * (1 / _framesCountForMovingToken));
				yield return null;
			}
		}
	}
}