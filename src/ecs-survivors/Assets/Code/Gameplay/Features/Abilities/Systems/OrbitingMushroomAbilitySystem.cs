using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;
using ModestTree;
using UnityEngine;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class OrbitingMushroomAbilitySystem : IExecuteSystem
    {
        private readonly  List<GameEntity> _buffer = new(1);
        private readonly IStaticDataService _staticDataService;
        private readonly IArmamentFactory _armamentFactory;
        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;

        public OrbitingMushroomAbilitySystem(GameContext game, IStaticDataService staticDataService, IArmamentFactory armamentFactory)
        {
            _staticDataService = staticDataService;
            _armamentFactory = armamentFactory;
            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.OrbitingMushroomAbility,
                    GameMatcher.CooldownUp));
            
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity ability  in _abilities.GetEntities(_buffer))
            foreach (GameEntity hero in _heroes)
            {
                AbilityLevel abilityLevel = _staticDataService
                    .GetAbilityLevel(AbilityId.OrbitingMushroom, 1);

                int projectileCount = abilityLevel.ProjectileSetup.ProjectileCount;

                for (int i = 0; i < projectileCount; i++)
                {
                    float phase = (2 * Mathf.PI * i) / projectileCount;
                    CreateProjectile(hero, phase, level: 1);
                }
                
                ability.PutCooldown(abilityLevel.Cooldown);
            }
        }

        private void CreateProjectile(GameEntity hero, float phase, int level)
        {
            _armamentFactory.CreateMushroom(level, hero.WorldPosition + Vector3.up, phase)
                .AddProducerId(hero.Id)
                .AddOrbitCenterPosition(hero.WorldPosition)
                .AddOrbitCenterFollowTarget(hero.Id)
                .isMoving = true;
        }
    }
}