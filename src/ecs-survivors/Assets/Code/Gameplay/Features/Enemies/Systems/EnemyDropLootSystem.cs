using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemyDropLootSystem : IExecuteSystem
    {
        private readonly ILootFactory _lootFactory;
        private readonly IGroup<GameEntity> _enemies;

        public EnemyDropLootSystem(GameContext game, ILootFactory lootFactory)
        {
            _lootFactory = lootFactory;
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition,
                    GameMatcher.ProcessingDeath
                    ));
        }

        public void Execute()
        {
            foreach (GameEntity enemy in _enemies)
            {
                LootTypeId lootType = LootTypeId.Unknown;
                
                if (Random.Range(0, 1f) <= 0.15)
                    lootType = LootTypeId.ExpGem;
                else if (Random.Range(0, 1f) <= 0.15)
                    lootType = LootTypeId.HealingItem;
                else if (Random.Range(0, 1f) <= 0.15)
                    lootType = LootTypeId.PoisonEnchantItem;
                else if (Random.Range(0, 1f) <= 0.15)
                    lootType = LootTypeId.ExplosionEnchantItem;
                    
                    
                if (lootType != LootTypeId.Unknown)
                {
                    _lootFactory.CreateLootItem(lootType, enemy.WorldPosition);
                }
            }
        }
    }
}