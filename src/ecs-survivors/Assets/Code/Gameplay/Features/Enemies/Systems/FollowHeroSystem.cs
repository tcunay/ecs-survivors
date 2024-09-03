using Entitas;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class FollowHeroSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly IGroup<GameEntity> _followers;
        private readonly IGroup<GameEntity> _heroes;

        public FollowHeroSystem(GameContext gameContext)
        {
            _followers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.FollowingToHero,
                    GameMatcher.Direction,
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
            foreach (GameEntity follower in _followers)
            {
                follower.isMoving = true;
            }
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                foreach (GameEntity follower in _followers)
                {
                    follower.ReplaceDirection((hero.WorldPosition - follower.WorldPosition).normalized);
                }
            }
        }
    }
}