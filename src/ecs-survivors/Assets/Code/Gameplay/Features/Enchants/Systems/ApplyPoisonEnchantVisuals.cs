using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public class ApplyPoisonEnchantVisuals : ReactiveSystem<GameEntity>
    {
        public ApplyPoisonEnchantVisuals(GameContext contextVar) : base(contextVar)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.AllOf(
                GameMatcher.EnchantVisuals,
                GameMatcher.Armament,
                GameMatcher.PoisonEnchant).Added());

        protected override bool Filter(GameEntity entity) => entity.isPoisonEnchant && entity.isArmament && entity.hasEnchantVisuals;

        protected override void Execute(List<GameEntity> armaments)
        {
            foreach (GameEntity armament in armaments)
            {
                armament.EnchantVisuals.ApplyPoison();
            }
        }
    }
}