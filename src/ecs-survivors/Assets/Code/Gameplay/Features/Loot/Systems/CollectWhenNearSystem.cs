using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectWhenNearSystem : IExecuteSystem
    {
        private const float CloseDistance = 0.2f;
        
        private readonly IGroup<GameEntity> _pullings;
        private readonly IGroup<GameEntity> _heroes;
        
        public CollectWhenNearSystem(GameContext game)
        {
            _pullings = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Pulling,
                    GameMatcher.WorldPosition
                ));
            
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition
                ));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            foreach (GameEntity pulling in _pullings)
            {
                if (Vector3.Distance(hero.WorldPosition, pulling.WorldPosition) <= CloseDistance)
                {
                    pulling.isCollected = true;
                }
            }
        }
    }
}