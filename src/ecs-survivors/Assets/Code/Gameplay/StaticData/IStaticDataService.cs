using System.Collections.Generic;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enchants;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Configs;
using Code.Gameplay.Windows;
using Code.Meta.Features.AfkGain.Configs;
using Code.Meta.UI.Shop.Items;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public interface IStaticDataService
  {
    void LoadAll();
    AbilityConfig GetAbilityConfig(AbilityId abilityId);
    LootConfig GetLootConfig(LootTypeId lootTypeId);
    EnchantConfig GetEnchantConfig(EnchantTypeId enchantTypeId);

    AbilityLevel GetAbilityLevel(AbilityId abilityId, int level);
    GameObject GetWindowPrefab(WindowId id);
    int MaxLevel();
    float ExperienceForLevel(int level);
    AfkGainConfig AfkGain { get; }
    ShopItemConfig GetShopItemConfig(ShopItemId shopItemId);
    List<ShopItemConfig> GetShopItemConfigs();
  }
}