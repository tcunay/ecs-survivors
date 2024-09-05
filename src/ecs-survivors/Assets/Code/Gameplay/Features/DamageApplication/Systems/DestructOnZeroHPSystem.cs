using Entitas;

namespace Code.Gameplay.Features.DamageApplication.Systems
{
    public class DestructOnZeroHPSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public DestructOnZeroHPSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher.CurrentHP);
        }
        
        public void Execute()
        {
            foreach (var entity in _entities)
            {
                if (entity.CurrentHP <= 0)
                {
                    entity.isDestructed = true;
                }
            }
        }
    }
}