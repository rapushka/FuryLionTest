using UnityEngine;

namespace Code.View
{
	public class RemainingActionsView : MonoBehaviour
	{
		[SerializeField] private TMPro.TextMeshProUGUI _remainingActionsText;
		
		public void UpdateView(int newValue) => _remainingActionsText.text = newValue.ToString();
	}
}