using Entitas;

namespace Code.Gameplay.Features.DamageApplication.Systems
{
    public class ApplyDamageOnTargetsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _damageDealers;
        private readonly GameContext _gameContext;

        public ApplyDamageOnTargetsSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            _damageDealers = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.TargetsBuffer,
                GameMatcher.Damage
            ));
        }
        
        public void Execute()
        {
            foreach (GameEntity damageDealer in _damageDealers)
            {
                foreach (int targetId in damageDealer.TargetsBuffer)
                {
                    GameEntity target = _gameContext.GetEntityWithId(targetId);

                    if (target.hasCurrentHP)
                    {
                        target.ReplaceCurrentHP(target.CurrentHP - damageDealer.Damage);

                        if (target.hasDamageTakenAnimator)
                        {
                            target.DamageTakenAnimator.PlayDamageTaken();
                        }
                    }
                    
                }
            }
        }
    }
}