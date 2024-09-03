using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Hero;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Input;
using Code.Infrastructure.Systems;

namespace Code.Gameplay
{
    public class BattleFeature : Feature
    {
        public BattleFeature(ISystemFactory systems)
        {
            Add(systems.Create<InputFeature>());
            
            Add(systems.Create<MovementFeature>());
            
            Add(systems.Create<HeroFeature>());
            
            Add(systems.Create<EnemyFeature>());
        }
    }
}