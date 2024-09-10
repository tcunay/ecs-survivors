using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.Cooldowns.Systems
{
    public class CooldownSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(32);

        public CooldownSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Cooldown,
                    GameMatcher.CooldownLeft));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                entity.ReplaceCooldownLeft(entity.CooldownLeft - _time.DeltaTime);

                if (entity.CooldownLeft <= 0)
                {
                    entity.isCooldownUp = true;
                    entity.RemoveCooldownLeft();
                }
            }
        }
    }
}