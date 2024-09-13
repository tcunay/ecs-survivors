using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.EffectApplication.Systems
{
    public class ApplyEffectsOnTargetsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _effectOwners;
        private readonly IEffectFactory _effectFactory;

        public ApplyEffectsOnTargetsSystem(GameContext gameContext, IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _effectOwners = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.TargetsBuffer,
                GameMatcher.EffectSetups
            ));
        }

        public void Execute()
        {
            foreach (GameEntity effectOwner in _effectOwners)
            foreach (int targetId in effectOwner.TargetsBuffer)
            foreach (EffectSetup setup in effectOwner.EffectSetups)
            {
                _effectFactory.CreateEffect(setup, ProducerId(effectOwner), targetId);
            }
        }

        private static int ProducerId(GameEntity effectOwner)
        {
            return effectOwner.hasProducerId ? effectOwner.ProducerId : effectOwner.Id;
        }
    }
}