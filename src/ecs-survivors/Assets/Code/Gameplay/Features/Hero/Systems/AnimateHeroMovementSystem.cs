using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class AnimateHeroMovementSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;

        public AnimateHeroMovementSystem(GameContext gameContext)
        {
            _heroes = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.HeroAnimator,
                    GameMatcher.Hero
                    ));
        }
        
        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                if (hero.isMoving)
                {
                    hero.HeroAnimator.PlayMove();
                }
                else
                {
                    hero.HeroAnimator.PlayIdle();
                }
            }
        }
    }
}