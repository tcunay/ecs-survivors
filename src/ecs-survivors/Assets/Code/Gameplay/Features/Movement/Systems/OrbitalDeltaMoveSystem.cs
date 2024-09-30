using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class OrbitalDeltaMoveSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _movers;

        public OrbitalDeltaMoveSystem(GameContext gameContext, ITimeService time)
        {
            _time = time;
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.OrbitPhase,
                    GameMatcher.OrbitCenterPosition,
                    GameMatcher.OrbitRadius,
                    GameMatcher.Speed,
                    GameMatcher.Moving,
                    GameMatcher.MovementAvailable,
                    GameMatcher.WorldPosition
                ));
        }
        
        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                float phase = mover.OrbitPhase + _time.DeltaTime * mover.Speed;
                mover.ReplaceOrbitPhase(phase);

                Vector3 newRelativePosition = new (
                    Mathf.Cos(phase) * mover.OrbitRadius,
                    Mathf.Sin(phase) * mover.OrbitRadius,
                    0);

                Vector3 newPosition = newRelativePosition + mover.OrbitCenterPosition;

                mover.ReplaceWorldPosition(newPosition);

            }
        }
    }
}