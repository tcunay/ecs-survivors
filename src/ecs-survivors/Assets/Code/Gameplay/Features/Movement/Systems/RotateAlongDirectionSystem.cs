using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class RotateAlongDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public RotateAlongDirectionSystem(GameContext gameContext)
        {
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Transform,
                    GameMatcher.RotationAlongDirection,
                    GameMatcher.Direction
                ));
        }
        
        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                if (mover.Direction.sqrMagnitude >= 0.01f)
                {
                    float angle = Mathf.Atan2(mover.Direction.y, mover.Direction.x) * Mathf.Rad2Deg;
                
                    mover.Transform.rotation = Quaternion.Euler(0,0, angle);
                }
            }
        }
    }
}