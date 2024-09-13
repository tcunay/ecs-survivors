using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class CleanupTargetBufferSystem : ICleanupSystem
    {
        private IGroup<GameEntity> _entities;

        public CleanupTargetBufferSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher.TargetsBuffer);
        }
        
        public void Cleanup()
        {
            foreach (GameEntity entity in _entities)
            {
                entity.TargetsBuffer.Clear();
            }
        }
    }
}