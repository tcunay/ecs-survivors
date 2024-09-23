using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class StatusDurationSystem : IExecuteSystem
    {
        private readonly List<GameEntity> _buffer = new(28);
        
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _statuses;

        public StatusDurationSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _statuses = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Duration,
                    GameMatcher.Status,
                    GameMatcher.TimeLeft
                    ));
        }

        public void Execute()
        {
            foreach (GameEntity status in _statuses.GetEntities(_buffer))
            {
                if (status.TimeLeft >= 0)
                    status.ReplaceTimeLeft(status.TimeLeft - _time.DeltaTime);
                else
                    status.isUnApplied = true;
            }
        }
    }
}