using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.ScenesTransfers
{
	[Serializable]
	public class SceneField
	{
		[SerializeField] private Object _scene;

		public string SceneName => _scene.name;

		public int BuildIndex => SceneManager.GetSceneByName(SceneName).buildIndex;
	}
}