using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Factory;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.Hero.Factory;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Applier;
using Code.Gameplay.Levels;
using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class InitializeHeroSystem : IInitializeSystem
    {
        private readonly IAbilityUpgradeService _upgradeService;

        public InitializeHeroSystem(
            IAbilityUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }
        
        public void Initialize()
        {
            _upgradeService.InitializeAbility(AbilityId.VegetableBolt);
        }
    }
}