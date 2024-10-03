using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectExperienceSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _collecteds;
        private readonly IGroup<GameEntity> _heroes;

        public CollectExperienceSystem(GameContext game)
        {
            _collecteds = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Collected,
                    GameMatcher.Experience
                ));
            
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero
                ));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            foreach (GameEntity collected in _collecteds)
            {
                hero.ReplaceExperience(hero.Experience + collected.Experience);
            }
        }
    }
}