using Code.Common.Systems.Destruct;
using Code.Infrastructure.Systems;

namespace Code.Meta
{
    public sealed class HomeScreenFeature : Feature
    {
        public HomeScreenFeature(ISystemFactory systems)
        {
            Add(systems.Create<ProcessDestructedFeature>());

        }
    }
}