using Code.Gameplay.Features.Abilities.Systems;
using Code.Gameplay.Features.Cooldowns.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Abilities
{
    public class AbilityFeature : Feature
    {
        public AbilityFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<CooldownSystem>());
            Add(systemFactory.Create<DestroyAbilityEntitiesOnUpgradeSystem>());
            
            Add(systemFactory.Create<VegetableBoltAbilitySystem>());
            Add(systemFactory.Create<OrbitingMushroomAbilitySystem>());
            Add(systemFactory.Create<GarlicAuraAbilitySystem>());
        }
    }
}