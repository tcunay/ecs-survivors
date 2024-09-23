using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems.StatusVisuals
{
    public class UnApplyPoisonVisualsSystem : ReactiveSystem<GameEntity>
    {
        public UnApplyPoisonVisualsSystem(GameContext context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher
                .AllOf(
                    GameMatcher.Poison,
                    GameMatcher.Status,
                    GameMatcher.UnApplied)
                .Added());

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStatus && entity.isPoison && entity.hasTargetId;
        }

        protected override void Execute(List<GameEntity> statuses)
        {
            foreach (GameEntity status in statuses)
            {
                GameEntity target = status.Target();

                if (target is {hasStatusVisuals: true})
                {
                    target.StatusVisuals.UnapplyPoison();
                }
            }
        }
    }
}