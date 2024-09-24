using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems.StatusVisuals
{
    public class ApplyFreezeVisualsSystem : ReactiveSystem<GameEntity>
    {
        public ApplyFreezeVisualsSystem(GameContext context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => 
            context.CreateCollector(GameMatcher.Freeze.Added());

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStatus && entity.isFreeze && entity.hasTargetId;
        }

        protected override void Execute(List<GameEntity> statuses)
        {
            foreach (GameEntity status in statuses)
            {
                GameEntity target = status.Target();

                if (target is {hasStatusVisuals: true})
                {
                    target.StatusVisuals.ApplyFreeze();
                }
            }
        }
    }
}