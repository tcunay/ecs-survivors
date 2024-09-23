using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class CleanupUnappliedStatuses : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _group;
        private readonly List<GameEntity> _buffer = new(32);

        public CleanupUnappliedStatuses(GameContext context)
        {
            _group = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.Status, 
                GameMatcher.UnApplied
            ));
        }

        public void Cleanup()
        {
            foreach (GameEntity status in _group.GetEntities(_buffer))
            {
                status.isDestructed = true;
            }
        }
    }
}