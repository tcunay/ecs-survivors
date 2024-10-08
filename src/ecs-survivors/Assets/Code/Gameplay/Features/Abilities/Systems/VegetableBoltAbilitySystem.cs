using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;
using ModestTree;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class VegetableBoltAbilitySystem : IExecuteSystem
    {
        private readonly  List<GameEntity> _buffer = new(1);
        private readonly IStaticDataService _staticDataService;
        private readonly IArmamentFactory _armamentFactory;
        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _enemies;

        public VegetableBoltAbilitySystem(GameContext game, IStaticDataService staticDataService, IArmamentFactory armamentFactory)
        {
            _staticDataService = staticDataService;
            _armamentFactory = armamentFactory;
            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.VegetableBoltAbility,
                    GameMatcher.CooldownUp));
            
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
            
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity ability  in _abilities.GetEntities(_buffer))
            foreach (GameEntity hero in _heroes)
            {
                if (_enemies.AsEnumerable().IsEmpty())
                {
                    continue;
                }
                
                _armamentFactory
                    .CreateVegetableBolt(1, hero.WorldPosition)
                    .AddProducerId(hero.Id)
                    .With(x => x.isMoving = true)
                    .ReplaceDirection((FirstAvailableTarget().WorldPosition - hero.WorldPosition).normalized);
                
                AbilityLevel abilityLevel = _staticDataService
                    .GetAbilityLevel(AbilityId.VegetableBolt, 1);
                
                ability.PutCooldown(abilityLevel.Cooldown);
            }
        }

        private GameEntity FirstAvailableTarget()
        {
            return _enemies.AsEnumerable().First();
        }
    }
}