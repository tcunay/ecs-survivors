using Code.Meta.UI.GoldHolder.Service;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Meta.UI.GoldHolder.Behaviours
{
    public class GoldHolder : MonoBehaviour
    {
        public TextMeshProUGUI Amount;
        
        private IStorageUIService _storageUIService;

        [Inject]
        private void Construct(IStorageUIService storageUIService)
        {
            _storageUIService = storageUIService;
        }
        
        private void Start()
        {
            _storageUIService.GoldChanged += UpdateGold;
            
            UpdateGold();
        }

        private void OnDestroy()
        {
            _storageUIService.GoldChanged -= UpdateGold;
        }

        private void UpdateGold() => Amount.text = _storageUIService.CurrentGold.ToString("0");
    }
}