using System;
using System.Collections;
using System.Collections.Generic;
using AppCharge.Monetization;
using UnityEngine;
using UnityEngine.UI;

namespace AppChargeTestAssignment.Scripts.Ui {
    public class StoreView : MonoBehaviour {
        [SerializeField] private Transform _productsContainer;
        [SerializeField] private ProductView _productViewPrefab;

        public void Initialize(IEnumerable<Product> products) {
            if (products == null) {
                return;
            }

            foreach (var product in products) {
                var productView = Instantiate(_productViewPrefab, _productsContainer);
                productView.InitializeWithProduct(product);
            }
        }
    }
}