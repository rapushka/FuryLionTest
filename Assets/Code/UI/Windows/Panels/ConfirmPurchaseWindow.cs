using Code.UI.Windows.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows.Panels
{
	public class ConfirmPurchaseWindow : UnityWindow
	{
		[SerializeField] private TextMeshProUGUI _coinsCountTextMesh;
		[SerializeField] private TextMeshProUGUI _priceTextMesh;
		[SerializeField] private Button _buttonYes;
		[SerializeField] private Button _buttonNo;

		private int _coinsCount;
		private int _price;

		private void OnEnable()
		{
			_buttonYes.onClick.AddListener(OnButtonYesClick);
			_buttonNo.onClick.AddListener(OnButtonNoClick);
		}

		private void OnDisable()
		{
			_buttonYes.onClick.RemoveListener(OnButtonYesClick);
			_buttonNo.onClick.RemoveListener(OnButtonNoClick);
		}

		public void Initialize(int coinsCount, int price)
		{
			_coinsCount = coinsCount;
			_price = price;
			
			_coinsCountTextMesh.text = _coinsCount.ToString();
			_priceTextMesh.text = _price.ToString();
		}

		private void OnButtonYesClick() => Result = WindowResult.Yes;

		private void OnButtonNoClick() => Result = WindowResult.No;
	}
}