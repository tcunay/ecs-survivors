using Code.Infrastructure.Systems;

namespace Code.Meta.UI.Shop.Systems
{
    public sealed class ShopFeature : Feature
    {
        public ShopFeature(ISystemFactory systems)
        {
            Add(systems.Create<BuyItemOnRequestSystem>());
            Add(systems.Create<ProcessBoughtItemSystem>());
        }
    }
}