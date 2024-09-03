using System.Collections.Generic;
using Entitas;

namespace Code.Common.Systems.Destruct.Systems
{
    public class CleanupGameDestructedSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(64);

        public CleanupGameDestructedSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher.Destructed);
        }
        
        public void Cleanup()
        {
            foreach (GameEntity gameEntity in _entities.GetEntities(_buffer))
            {
                gameEntity.Destroy();
            }
        }
    }
}