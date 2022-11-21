﻿using Code.Gameplay.Coins;
using Code.UI.Windows.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
		private bool _tryBuy;
		private CoinsCounter _coins;
		private WindowsChain _windowsChain;

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

		[Inject]
		public void Construct(CoinsCounter coins, WindowsChain windowsChain)
		{
			_coins = coins;
			_windowsChain = windowsChain;
		}

		public void Initialize(int coinsCount, int price, bool tryBuy = false)
		{
			_coinsCount = coinsCount;
			_price = price;
			_tryBuy = tryBuy;

			_coinsCountTextMesh.text = _coinsCount.ToString();
			_priceTextMesh.text = _price.ToString();
		}

		private void OnButtonYesClick()
		{
			if (_tryBuy == false)
			{
				CloseWithResult(WindowResult.Yes);
			}
			else
			{
				if (_coins.TrySpent(_price))
				{
					CloseWithResult(WindowResult.Yes);
				}
				else
				{
					_windowsChain.Open<NotEnoughMoneyWindow>();
				}
			}
		}

		private void OnButtonNoClick() => CloseWithResult(WindowResult.No);

		private void CloseWithResult(WindowResult windowResult)
		{
			Result = windowResult;
			_windowsChain.Close();
		}
	}
}