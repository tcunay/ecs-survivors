using Entitas;
using UnityEngine;

namespace Code.Common.Systems.Destruct.Systems
{
    public class CleanupGameDestructedViewsSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public CleanupGameDestructedViewsSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Destructed,
                    GameMatcher.View
                ));
        }
        
        public void Cleanup()
        {
            foreach (GameEntity gameEntity in _entities)
            {
                gameEntity.View.ReleaseEntity();
                Object.Destroy(gameEntity.View.gameObject);
            }
        }
    }
}