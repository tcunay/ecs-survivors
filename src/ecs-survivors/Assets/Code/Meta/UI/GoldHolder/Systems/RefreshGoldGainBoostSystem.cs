using System.Collections.Generic;
using System.Linq;
using Code.Meta.UI.GoldHolder.Service;
using Entitas;

namespace Code.Meta.UI.GoldHolder.Systems
{
    public class RefreshGoldGainBoostSystem : ReactiveSystem<MetaEntity>, IInitializeSystem
    {
        private readonly IStorageUIService _storage;
        private readonly IGroup<MetaEntity> _boosters;
        
        private readonly List<MetaEntity> _boostersBuffer = new();

        public RefreshGoldGainBoostSystem(MetaContext meta, IStorageUIService storage) : base(meta)
        {
            _storage = storage;

            _boosters = meta.GetGroup(MetaMatcher.GoldGainBoost);
        }
        
        public void Initialize()
        {
            UpdateGoldGainBoost(_boosters.GetEntities(_boostersBuffer));
        } 

        protected override ICollector<MetaEntity> GetTrigger(IContext<MetaEntity> context) =>
            context.CreateCollector(MetaMatcher.GoldGainBoost.AddedOrRemoved());

        protected override bool Filter(MetaEntity entity) => true;

        protected override void Execute(List<MetaEntity> boosters)
        {
            UpdateGoldGainBoost(boosters);
        }

        private void UpdateGoldGainBoost(List<MetaEntity> boosters)
        {
            float goldGainBoost = 0;
            
            foreach (MetaEntity booster in boosters)
            {
                if (booster.hasGoldGainBoost)
                {
                    goldGainBoost += booster.GoldGainBoost;
                }
            }
            
            _storage.UpdateGoldGainBoost(goldGainBoost);
        }
    }
}