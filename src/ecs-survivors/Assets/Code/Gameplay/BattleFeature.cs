using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Movement;

namespace Code.Gameplay
{
    public class BattleFeature : Feature
    {
        public BattleFeature(GameContext gameContext, ITimeService timeService)
        {
            Add(new MovementFeature(gameContext, timeService));
        }
    }
}