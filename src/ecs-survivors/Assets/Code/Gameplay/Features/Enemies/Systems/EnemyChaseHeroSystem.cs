using Entitas;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemyChaseHeroSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly IGroup<GameEntity> _chasers;
        private readonly IGroup<GameEntity> _heroes;

        public EnemyChaseHeroSystem(GameContext gameContext)
        {
            _chasers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition
                ));

            _heroes = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition
                ));
        }

        public void Initialize()
        {
            foreach (GameEntity follower in _chasers)
            {
                follower.isMoving = true;
            }
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                foreach (GameEntity chaser in _chasers)
                {
                    chaser.ReplaceDirection((hero.WorldPosition - chaser.WorldPosition).normalized);
                }
            }
        }
    }
}