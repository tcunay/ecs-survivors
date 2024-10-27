using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Meta.Features.Simulation.Systems
{
    public class CalculateGoldGainSystem : IExecuteSystem, ISystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<MetaEntity> _boosters;
        private readonly IGroup<MetaEntity> _storages;

        public CalculateGoldGainSystem(MetaContext game, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _boosters = game.GetGroup(MetaMatcher.AllOf(MetaMatcher.GoldGainBoost));
            _storages = game.GetGroup(MetaMatcher.AllOf(MetaMatcher.Storage, MetaMatcher.GoldPerSecond));
        }

        public void Execute()
        {
            foreach (MetaEntity storage in _storages)
            {
                float num = 1f;
                foreach (MetaEntity booster in _boosters)
                    num += booster.GoldGainBoost;
                
                storage.ReplaceGoldPerSecond(_staticDataService.AfkGain.GoldPerSecond * num);
            }
        }
    }
}