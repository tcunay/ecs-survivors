using Code.Gameplay.Features.Enemies.Systems;

namespace Code.Gameplay.Features.Enemies
{
    public class EnemyFeature : Feature
    {
        public EnemyFeature(GameContext gameContext)
        {
            Add(new FollowHeroSystem(gameContext));
        }
    }
}