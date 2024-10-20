using System;
using Code.Gameplay.Features.LevelUp.Behaviours;
using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.LevelUp.Windows
{
    public class AbilityUIFactory : IAbilityUIFactory
    {
        private const string AbilityCardPrefabPath = "UI/Abilities/AbilityCard";
        
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public AbilityUIFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public AbilityCard CreateAbilityCard(Transform parent)
        {
            AbilityCard abilityCard = _assetProvider.LoadAsset<AbilityCard>(AbilityCardPrefabPath);

            if (abilityCard == null)
            {
                throw new ArgumentNullException();
            }
            return _instantiator.InstantiatePrefabForComponent<AbilityCard>(
                abilityCard, parent);
        }
    }
}