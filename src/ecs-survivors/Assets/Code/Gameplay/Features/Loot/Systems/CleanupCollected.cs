using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CleanupCollected : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _collecteds;

        public CleanupCollected(GameContext contextParameter)
        {
            _collecteds = contextParameter.GetGroup(GameMatcher.AllOf(GameMatcher.Collected));
        }

        public void Cleanup()
        {
            foreach (GameEntity collected in _collecteds)
            {
                collected.isDestructed = true;
            }
        }
    }
}