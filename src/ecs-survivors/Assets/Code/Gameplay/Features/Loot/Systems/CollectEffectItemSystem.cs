using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectEffectItemSystem : IExecuteSystem
    {
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _collecteds;
        private readonly IGroup<GameEntity> _heroes;

        public CollectEffectItemSystem(GameContext game, IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _collecteds = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Collected,
                    GameMatcher.EffectSetups
                ));
            
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.Id,
                    GameMatcher.WorldPosition
                ));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            foreach (GameEntity collected in _collecteds)
            foreach (EffectSetup effectSetup in collected.EffectSetups)
            {
                _effectFactory.CreateEffect(effectSetup, hero.Id, hero.Id);
            }
        }
    }
}