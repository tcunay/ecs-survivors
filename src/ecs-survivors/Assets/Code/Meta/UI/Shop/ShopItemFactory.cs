using System;
using Code.Common.Entity;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using Code.Meta.UI.Shop.Items;
using Code.Meta.UI.Shop.Systems;

namespace Code.Meta.UI.Shop
{
    public class ShopItemFactory : IShopItemFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IIdentifierService _identifierService;

        public ShopItemFactory(IStaticDataService staticData, IIdentifierService identifierService)
        {
            _staticData = staticData;
            _identifierService = identifierService;
        }

        public MetaEntity CreateShopItem(ShopItemId shopItemId)
        {
            ShopItemConfig config = _staticData.GetShopItemConfig(shopItemId);

            return config.Kind switch
            {
                ShopItemKind.Booster => CreateMetaEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddGoldGainBoost(config.Boost)
                    .AddDuration(config.Duration),
                
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}