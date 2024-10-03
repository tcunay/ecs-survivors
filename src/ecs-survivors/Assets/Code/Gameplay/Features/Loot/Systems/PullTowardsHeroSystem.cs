using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class PullTowardsHeroSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _pullings;
        private readonly IGroup<GameEntity> _heroes;

        public PullTowardsHeroSystem(GameContext game)
        {
            _pullings = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Pulling,
                    GameMatcher.WorldPosition
                ));
            
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition
                ));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            foreach (GameEntity pulling in _pullings)
            {
                pulling.ReplaceDirection((hero.WorldPosition - pulling.WorldPosition).normalized);
                pulling.ReplaceSpeed(4);
                pulling.isMoving = true;
                pulling.isMovementAvailable = true;
            }
        }
    }
}