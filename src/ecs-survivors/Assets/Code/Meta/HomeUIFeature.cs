using Code.Infrastructure.Systems;
using Code.Meta.UI.GoldHolder.Systems;
using Code.Meta.UI.Shop.Systems;

namespace Code.Meta
{
    public sealed class HomeUIFeature : Feature
    {
        public HomeUIFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializePurchasedItemSystem>());

            Add(systems.Create<RefreshGoldGainBoostSystem>());
            Add(systems.Create<RefreshGoldSystem>());
        }
    }
}