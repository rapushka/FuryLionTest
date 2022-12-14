using System.Collections;
using Code.Gameplay.Tokens;
using Code.Inner.CustomMonoBehaviours;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.TokensField.GravityBehaviour
{
	public class TokensViewsMover
	{
		private readonly float _framesCountForMovingToken;
		private readonly CoroutinesHandler _coroutines;
		private readonly WaitForSeconds _waitForOneMillisecond;

		[Inject]
		public TokensViewsMover(CoroutinesHandler coroutines)
		{
			_coroutines = coroutines;
			_framesCountForMovingToken = 10f;
			_waitForOneMillisecond = new WaitForSeconds(0.01f);
		}

		public void MoveView(Token token, Vector3 to) => _coroutines.StartCoroutine(MoveRoutine(token, to));

		private IEnumerator MoveRoutine(Component token, Vector3 to)
		{
			for (var i = 0; i < _framesCountForMovingToken; i++)
			{
				token.transform.Translate(to * (1 / _framesCountForMovingToken));
				yield return _waitForOneMillisecond;
			}
		}
	}
}