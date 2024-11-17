using System;
using Code.Meta.UI.GoldHolder.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.Shop.Items
{
    public class ShopItem : MonoBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI Price;
        public TextMeshProUGUI Duration;
        public TextMeshProUGUI Boost;
        public Button BuyButton;
        public CanvasGroup CanvasGroup;

        public Color EnoughColor;
        public Color NotEnoughColor;
        
        private bool _isAvailable;
        private float _price;
        private float _currentGold;
        private IStorageUIService _storage;

        private bool EnoughGold => _currentGold >= _price;


        [Inject]
        private void Construct(IStorageUIService storage)
        {
            _storage = storage;
        }

        public void Setup(ShopItemConfig config)
        {
            Icon.sprite = config.Icon;
            Price.text = config.Price.ToString();
            Duration.text = TimeSpan.FromSeconds(config.Duration).ToString("m'm 's's'");
            Boost.text = config.Boost.ToString("+0%");

            _price = config.Price;
        }

        private void Start()
        {
            _storage.GoldChanged += UpdatePriceThreshold; 
            
            UpdatePriceThreshold();
        }

        private void OnDestroy() => _storage.GoldChanged -= UpdatePriceThreshold;

        public void UpdateAvailability(bool value)
        {
            _isAvailable = value;

            CanvasGroup.alpha = _isAvailable ? 1 : 0.7f;

            RefreshBuyButton();
        }

        private void UpdatePriceThreshold()
        {
            _currentGold = _storage.CurrentGold;
            
            Price.color = EnoughGold ? EnoughColor : NotEnoughColor;
            
            RefreshBuyButton();
        }
        
        private void RefreshBuyButton()
        {
            BuyButton.interactable = EnoughGold && _isAvailable;
        }
    }
}