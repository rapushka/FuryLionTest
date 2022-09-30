using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
	public class SceneTransfer
	{
		[Inject] public SceneTransfer() { }

		public void ToLoseScene()
		{
			Debug.Log("Level lost");
		}
	}
}