using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Inner.CustomMonoBehaviours
{
	public class CoroutinesHandler : MonoBehaviour
	{
		private readonly List<Coroutine> _startedRoutines = new();

		public void OnSceneChanged()
		{
			StopAllCoroutines();
			_startedRoutines.Clear();
		}

		public int StartRoutine(IEnumerator routine)
		{
			_startedRoutines.Add(StartCoroutine(routine));
			return _startedRoutines.Count - 1;
		}

		public void StopRoutine(int indexOfCoroutine)
		{
			var startedCoroutine = _startedRoutines[indexOfCoroutine];
			
			if (startedCoroutine != null)
			{
				StopCoroutine(startedCoroutine);
			}

			_startedRoutines[indexOfCoroutine] = null;
		}
	}
}