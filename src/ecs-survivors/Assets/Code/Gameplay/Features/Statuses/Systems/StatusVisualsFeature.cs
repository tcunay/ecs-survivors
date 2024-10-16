using Code.Gameplay.Features.Enchants.Systems;
using Code.Gameplay.Features.Statuses.Systems.StatusVisuals;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class StatusVisualsFeature : Feature
    {
        public StatusVisualsFeature(ISystemFactory systems)
        {
            Add(systems.Create<ApplyPoisonVisualsSystem>());
            Add(systems.Create<ApplyFreezeVisualsSystem>());
            
            Add(systems.Create<UnApplyPoisonVisualsSystem>());
            Add(systems.Create<UnApplyFreezeVisualsSystem>());
            
            Add(systems.Create<RemoveUnappliedEnchantsFromHolder>());
        }
    }
}