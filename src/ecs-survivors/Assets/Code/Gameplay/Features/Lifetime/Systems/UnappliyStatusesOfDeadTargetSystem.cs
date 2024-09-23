using Entitas;

namespace Code.Gameplay.Features.Lifetime.Systems
{
    public class UnappliyStatusesOfDeadTargetSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _statuses;
        private readonly IGroup<GameEntity> _dead;

        public UnappliyStatusesOfDeadTargetSystem(GameContext game)
        {
            _statuses = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Status, GameMatcher.TargetId));
            
            _dead = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Dead, GameMatcher.Id));
        }

        public void Execute()
        {
            foreach (GameEntity deadEntity in _dead)
            foreach (GameEntity status in _statuses)
            {
                if (status.TargetId == deadEntity.Id) 
                    status.isUnApplied = true;
            }
        }
    }
}