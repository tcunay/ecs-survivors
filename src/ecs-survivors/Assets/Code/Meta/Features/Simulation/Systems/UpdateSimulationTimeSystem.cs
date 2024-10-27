using Code.Progress.Provider;
using Entitas;

namespace Code.Meta.Features.Simulation.Systems
{
    public class UpdateSimulationTimeSystem : IExecuteSystem
    {
        private readonly IProgressProvider _progressProvider;
        private readonly IGroup<MetaEntity> _tick;

        public UpdateSimulationTimeSystem(MetaContext game, IProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
            _tick = game.GetGroup(MetaMatcher
                .AllOf(MetaMatcher.Tick));
        }

        public void Execute()
        {
            foreach (MetaEntity tick in _tick)
            {
                _progressProvider.ProgressData.LastSimulationTickTime = 
                    _progressProvider.ProgressData.LastSimulationTickTime
                        .AddSeconds(tick.Tick);
            }
        }
    }
}