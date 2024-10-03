using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enchants;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Configs;

namespace Code.Gameplay.StaticData
{
  public interface IStaticDataService
  {
    void LoadAll();
    AbilityConfig GetAbilityConfig(AbilityId abilityId);
    LootConfig GetLootConfig(LootTypeId lootTypeId);
    EnchantConfig GetEnchantConfig(EnchantTypeId enchantTypeId);

    AbilityLevel GetAbilityLevel(AbilityId abilityId, int level);
  }
}