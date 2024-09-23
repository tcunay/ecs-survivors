using Code.Common.Extensions;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Applier;
using Code.Gameplay.Features.Statuses.Factory;
using Entitas;

namespace Code.Gameplay.Features.EffectApplication.Systems
{
    public class ApplyStatusesOnTargetsSystem : IExecuteSystem
    {
        private readonly IStatusApplier _statusApplier;
        private readonly IGroup<GameEntity> _statusOwners;

        public ApplyStatusesOnTargetsSystem(GameContext gameContext, IStatusApplier statusApplier)
        {
            _statusApplier = statusApplier;
            _statusOwners = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.TargetsBuffer,
                GameMatcher.StatusSetups 
            ));
        }

        public void Execute()
        {
            foreach (GameEntity owner in _statusOwners)
            foreach (int targetId in owner.TargetsBuffer)
            foreach (StatusSetup setup in owner.StatusSetups)
            {
                _statusApplier.ApplyStatus(setup, ProducerId(owner), targetId)
                    .With(x => x.isApplied = true);
            }
        }

        private static int ProducerId(GameEntity effectOwner)
        {
            return effectOwner.hasProducerId ? effectOwner.ProducerId : effectOwner.Id;
        }
    }
}