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
        private readonly IHeroFactory _heroFactory;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IAbilityUpgradeService _upgradeService;

        public InitializeHeroSystem(
            IHeroFactory heroFactory,
            ILevelDataProvider levelDataProvider,
            IAbilityUpgradeService upgradeService)
        {
            _heroFactory = heroFactory;
            _levelDataProvider = levelDataProvider;
            _upgradeService = upgradeService;
        }
        
        public void Initialize()
        {
            GameEntity hero = _heroFactory.CreateHero(_levelDataProvider.StartPoint);
            _upgradeService.InitializeAbility(AbilityId.VegetableBolt);
            /*_abilityFactory.CreateVegetableBoltAbility(level: 1);
            _abilityFactory.CreateOrbitingMushroomAbility(level: 1);
            _abilityFactory.CreateGarlicAuraAbility();*/

            /*_statusApplier.ApplyStatus(new StatusSetup()
            {
                StatusTypeId = StatusTypeId.PoisonEnchant,
                Duration = 10, 
                
            }, hero.Id, hero.Id);*/
            
            /*_statusApplier.ApplyStatus(new StatusSetup()
            {
                StatusTypeId = StatusTypeId.ExplosiveEnchant,
                Duration = 10, 
                
            }, hero.Id, hero.Id);*/
        }
    }
}