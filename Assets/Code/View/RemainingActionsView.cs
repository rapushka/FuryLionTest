using UnityEngine;
using Zenject;

namespace Code.View
{
	public class RemainingActionsView : MonoBehaviour
	{
		[SerializeField] private TMPro.TextMeshProUGUI _remainingActionsText;
		
		public void OnRemainingActionsUpdateSignal(int newValue)
		{
			_remainingActionsText.text = newValue.ToString();
		}
	}
}