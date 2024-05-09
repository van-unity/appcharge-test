using AppCharge.Monetization;
using AppCharge.Monetization.EventArgs;
using AppChargeTestAssignment.Scripts.Ui;
using UnityEngine;

namespace AppChargeTestAssignment.Scripts {
    public class GameController : MonoBehaviour {
        [SerializeField] private StoreView _storeViewPrefab;
        [SerializeField] private MessagePopup _messagePopupPrefab;
        [SerializeField] private GameObject _purchaseProcessView;

        private void Start() {
            AppChargeManager.Instance.Initialize();
            ProductView.PurchaseClicked += ProductViewOnPurchaseClicked;
            AppChargeManager.Instance.StoreManager.ProductPurchased += StoreManagerOnProductPurchased;
            AppChargeManager.Instance.StoreManager.ProductPurchaseFailed += StoreManagerOnProductPurchaseFailed;
            
            var storeView = Instantiate(_storeViewPrefab);
            storeView.Initialize(AppChargeManager.Instance.ProductRepository.EnumerateProducts());
        }

        private void StoreManagerOnProductPurchaseFailed(ProductPurchaseFailedEventArgs args) {
            var popup = Instantiate(_messagePopupPrefab);
            popup.Initialize($"Purchase Failed!\n{args.Reason}");
        }

        private void StoreManagerOnProductPurchased(ProductPurchasedEventArgs args) {
            var purchasedProduct = AppChargeManager.Instance.ProductRepository.GetProductByID(args.ProductID);
            var popup = Instantiate(_messagePopupPrefab);
            popup.Initialize($"{purchasedProduct.ProductName} {purchasedProduct.Description} \nis Purchased!");
        }

        private async void ProductViewOnPurchaseClicked(string productId) {
            _purchaseProcessView.SetActive(true);
            await AppChargeManager.Instance.StoreManager.PurchaseProductAsync(productId);
            _purchaseProcessView.SetActive(false);
        }

        private void OnDestroy() {
            ProductView.PurchaseClicked -= ProductViewOnPurchaseClicked;
            AppChargeManager.Instance.StoreManager.ProductPurchased -= StoreManagerOnProductPurchased;
            AppChargeManager.Instance.StoreManager.ProductPurchaseFailed -= StoreManagerOnProductPurchaseFailed;
        }
    }
}