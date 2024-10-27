using Entitas;

namespace Code.Meta.Features.Simulation.Systems
{
    public class AfkGoldGainSystem : IExecuteSystem
    {
        private readonly IGroup<MetaEntity> _tick;
        private readonly IGroup<MetaEntity> _storages;

        public AfkGoldGainSystem(MetaContext game)
        {
            _tick = game.GetGroup(MetaMatcher
                .AllOf(MetaMatcher.Tick));
            
            _storages = game.GetGroup(MetaMatcher
                .AllOf(
                    MetaMatcher.Storage,
                    MetaMatcher.Gold,
                    MetaMatcher.GoldPerSecond
                    ));
        }

        public void Execute()
        {
            foreach (MetaEntity tick in _tick)
            foreach (MetaEntity storage in _storages)
            {
                storage.ReplaceGold(storage.Gold + tick.Tick * storage.GoldPerSecond);
            }
        }
    }
}