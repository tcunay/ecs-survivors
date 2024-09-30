using System.Collections.Generic;
using Code.Common.Entity;
using Code.Gameplay.Features.CharacterStats;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class ApplySpeedUpStatusSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _speedUpStatuses;
        private readonly List<GameEntity> _buffer = new(32);

        public ApplySpeedUpStatusSystem(GameContext game)
        {
            _speedUpStatuses = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.Status,
                    GameMatcher.SpeedUp,
                    GameMatcher.ProducerId,
                    GameMatcher.TargetId,
                    GameMatcher.EffectValue
                )
                .NoneOf(GameMatcher.Affected));
        }

        public void Execute()
        {
            foreach (GameEntity status in _speedUpStatuses.GetEntities(_buffer))
            {
                CreateEntity.Empty()
                    .AddStatChange(Stats.Speed)
                    .AddTargetId(status.TargetId)
                    .AddProducerId(status.ProducerId)
                    .AddEffectValue(status.EffectValue)
                    .AddApplierStatusLink(status.Id)
                    ;

                status.isAffected = true;
            }
        }
    }
}