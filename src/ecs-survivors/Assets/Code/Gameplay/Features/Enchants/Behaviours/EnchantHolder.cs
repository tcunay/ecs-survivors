using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Enchants.UIFactories;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Enchants.Behaviours
{
    public class EnchantHolder : MonoBehaviour
    {
        public Transform EnchantLayout;

        private readonly List<Enchant> _enchants = new();
        
        private IEnchantUIFactory _factory;

        [Inject]
        private void Construct(IEnchantUIFactory factory)
        {
            _factory = factory;
        }

        public void AddEnchant(EnchantTypeId typeId)
        {
            if (EnchantAlreadyHeld(typeId))
            {
                return;
            }
            
            Debug.Log($"Add Enchant = {typeId}");
            
            Enchant enchant = _factory.CreateEnchant(EnchantLayout, typeId);

            _enchants.Add(enchant);
        }

        public void RemoveEnchant(EnchantTypeId typeId)
        {
            Enchant enchant = _enchants.Find(enchant => enchant.Id == typeId);

            if (enchant != null)
            {
                Debug.Log($"Remove Enchant = {typeId}");
                _enchants.Remove(enchant);
                Destroy(enchant.gameObject);
            }
        }

        private bool EnchantAlreadyHeld(EnchantTypeId typeId)
        {
            return _enchants.Any(x => x.Id == typeId);
        }
    }
}