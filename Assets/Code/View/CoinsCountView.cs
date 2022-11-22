using TMPro;
using UnityEngine;

namespace Code.View
{
	public class CoinsCountView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _textMesh;
		
		public void UpdateView(int newValue) => _textMesh.text = newValue.ToString();
	}
}