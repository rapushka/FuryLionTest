using UnityEngine;

namespace Code.Levels
{
	[CreateAssetMenu(fileName = "Level ", menuName = "ScriptableObjects/Level", order = 0)]
	public class Level : ScriptableObject
	{
		[SerializeField] private ArrayLayout _tokens;
	}
}