using Code.Gameplay.Common.Visuals.Enchants;
using Entitas;

namespace Code.Gameplay.Features.Enchants
{
    [Game] public class EnchantTypeIdComponent : IComponent { public EnchantTypeId Value; }
    [Game] public class EnchantVisualsComponent : IComponent { public EnchantVisuals Value; }
    [Game] public class PoisonEnchant : IComponent {  }
}