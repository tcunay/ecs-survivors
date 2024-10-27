using Code.Common.Systems.Destruct;
using Code.Infrastructure.Systems;
using Code.Meta.Features.Simulation;
using Code.Meta.Features.Simulation.Systems;

namespace Code.Meta
{
    public sealed class HomeScreenFeature : Feature
    {
        public HomeScreenFeature(ISystemFactory systems)
        {
            Add(systems.Create<EmitTickSystem>(MetaConstants.SimulationTickSeconds));

            Add(systems.Create<SimulationFeature>());

            Add(systems.Create<CleanupTickSystem>());
            Add(systems.Create<ProcessDestructedFeature>());
        }
    }
}