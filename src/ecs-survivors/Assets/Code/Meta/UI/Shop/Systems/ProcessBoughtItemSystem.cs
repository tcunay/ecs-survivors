using System.Collections.Generic;
using Code.Progress.SaveLoad;
using Entitas;

namespace Code.Meta.UI.Shop.Systems
{
    public class ProcessBoughtItemSystem : ReactiveSystem<MetaEntity>
    {
        private readonly IShopItemFactory _shopItemFactory;
        private readonly ISaveLoadService _saveLoadService;

        public ProcessBoughtItemSystem(MetaContext meta, IShopItemFactory shopItemFactory, ISaveLoadService saveLoadService) : base(meta)
        {
            _shopItemFactory = shopItemFactory;
            _saveLoadService = saveLoadService;
        }

        protected override ICollector<MetaEntity> GetTrigger(IContext<MetaEntity> context) =>
            context.CreateCollector(MetaMatcher.Purchased.Added());

        protected override bool Filter(MetaEntity entity) => entity.hasShopItemId;

        protected override void Execute(List<MetaEntity> purchases)
        {
            foreach (MetaEntity purchase in purchases)
            {
                _shopItemFactory.CreateShopItem(purchase.ShopItemId);
            }
            
            _saveLoadService.SaveProgress();
        }
    }
}