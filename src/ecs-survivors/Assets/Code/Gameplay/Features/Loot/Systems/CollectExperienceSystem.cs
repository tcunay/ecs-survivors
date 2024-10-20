using Code.Gameplay.Features.LevelUp.Services;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectExperienceSystem : IExecuteSystem
    {
        private readonly ILevelUpService _levelUpService;
        private readonly IGroup<GameEntity> _collecteds;
        private readonly IGroup<GameEntity> _heroes;

        public CollectExperienceSystem(GameContext game, ILevelUpService levelUpService)
        {
            _levelUpService = levelUpService;
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
                _levelUpService.AddExperience(collected.Experience);
                hero.ReplaceExperience(_levelUpService.CurrentExperience);
            }
        }
    }
}