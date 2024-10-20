using Entitas;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class DestroyAbilityEntitiesOnUpgradeSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _upgradeRequests;
        private readonly IGroup<GameEntity> _abilities;

        public DestroyAbilityEntitiesOnUpgradeSystem(GameContext game)
        {
            _game = game;
            _upgradeRequests = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.AbilityId,
                    GameMatcher.UpgradeRequest));
            
            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.AbilityId,
                    GameMatcher.RecreatedOnUpgrade));
        }

        public void Execute()
        {
            foreach (GameEntity request in _upgradeRequests)
            foreach (GameEntity ability in _abilities)
            {
                if (request.AbilityId == ability.AbilityId)
                {
                    foreach (GameEntity entity in _game.GetEntitiesWithParentAbility(ability.AbilityId))
                    {
                        entity.isDestructed = true;
                    }

                    ability.isActive = false;
                }
                
            }
        }
    }
}