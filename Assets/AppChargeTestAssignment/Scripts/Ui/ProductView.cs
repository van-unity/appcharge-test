using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AppCharge.Monetization;

namespace AppChargeTestAssignment.Scripts.Ui {
    public class ProductView : MonoBehaviour {
        private const string PRICE_FORMAT = "{0}$";

        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Button _purchaseButton;

        public static event Action<string> PurchaseClicked;

        private string _productID;

        public void InitializeWithProduct(Product product) {
            _productID = product.ID;
            _priceText.text = string.Format(PRICE_FORMAT, product.Price);
            _descriptionText.text = product.Description;
        }

        private void OnEnable() {
            _purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
        }

        private void OnPurchaseButtonClicked() {
            PurchaseClicked?.Invoke(_productID);
        }

        private void OnDisable() {
            _purchaseButton.onClick.RemoveListener(OnPurchaseButtonClicked);
        }
    }
}