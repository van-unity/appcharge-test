using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AppChargeTestAssignment.Scripts.Ui {
    public class MessagePopup : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _closeButton;

        public void Initialize(string message) {
            _messageText.text = message;
        }

        private void Start() {
            _closeButton.onClick.AddListener(() => Destroy(gameObject));
        }
    }
}