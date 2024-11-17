using System;
using System.Collections.Generic;
using Code.Meta.UI.Shop.Items;

namespace Code.Meta.UI.Shop.Service
{
    public interface IShopUIService
    {
        event Action ShopChanged;
        List<ShopItemConfig> GetAvailableShopItems();
        void UpdatePurchasedItems(IEnumerable<ShopItemId> purchasedItems);
        void Cleanup();
        ShopItemConfig GetConfig(ShopItemId requestShopItemId);
        void UpdatePurchasedItem(ShopItemId requestShopItemId);
    }
}