using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.CharacterStats;
using Code.Gameplay.Features.Effects;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IIdentifierService _identifierService;

        public EnemyFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateEnemy(EnemyTypeId typeId, Vector3 at)
        {
            switch (typeId)
            {
                case EnemyTypeId.Goblin:
                    return CreateGoblin(at);
            }

            throw new Exception($"Enemy with {typeId} does not exist");
        }

        private GameEntity CreateGoblin(Vector2 at)
        {
            Dictionary<Stats, float> baseStats = InitStats.EmptyStatDictionary()
                    .With(x => x[Stats.Speed] = 1)
                    .With(x => x[Stats.MaxHp] = 3)
                    .With(x => x[Stats.Damage] = 1)
                ;
            
            return CreateEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddEnemyTypeId(EnemyTypeId.Goblin)
                    .AddWorldPosition(at)
                    .AddDirection(Vector2.zero)
                    .AddBaseStats(baseStats)
                    .AddStatModifiers(InitStats.EmptyStatDictionary())
                    .AddSpeed(baseStats[Stats.Speed])
                    .AddMaxHP(baseStats[Stats.MaxHp])
                    .AddCurrentHP(baseStats[Stats.MaxHp])
                    .AddEffectSetups(new List<EffectSetup>{new() {EffectTypeId = EffectTypeId.Damage, Value = baseStats[Stats.Damage]}})
                    .AddRadius(0.3f)
                    .AddCollectTargetsInterval(0.5f)
                    .AddCollectTargetsTimer(0)
                    .AddTargetsBuffer(new List<int>(1))
                    .AddLayerMask(CollisionLayer.Hero.AsMask())
                    .AddViewPath("Gameplay/Enemies/Goblins/Torch/goblin_torch_blue")
                    .With(x => x.isEnemy = true)
                    .With(x => x.isTurnedAlongDirection = true)
                    .With(x => x.isMovementAvailable = true)
                ;
        }
    }
}