using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Features.Effects.Factory
{
    public class EffectFactory : IEffectFactory
    {
        private readonly IIdentifierService _identifierService;

        public EffectFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateEffect(EffectSetup setup, int producerId, int targetId )
        {
            switch (setup.EffectTypeId)
            {
                case EffectTypeId.Damage:
                    return CreateDamage(producerId, targetId, setup.Value);
            }
            
            throw new Exception($"Effect with Type Id = {setup.EffectTypeId} does not exist");
        }

        private GameEntity CreateDamage(int producerId, int targetId, float value)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .With(x => x.isEffect = true)
                .With(x => x.isDamageEffect = true)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .AddEffectValue(value);
        }
    }
}