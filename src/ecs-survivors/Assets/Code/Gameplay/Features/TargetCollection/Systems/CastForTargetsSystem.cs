using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Physics;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class CastForTargetsSystem : IExecuteSystem
    {
        private readonly IPhysicsService _physicsService;
        private readonly IGroup<GameEntity> _ready;
        private readonly List<GameEntity> _buffer = new(64);

        public CastForTargetsSystem(GameContext gameContext, IPhysicsService physicsService)
        {
            _physicsService = physicsService;
            _ready = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ReadyToCollectTargets,
                    GameMatcher.TargetsBuffer,
                    GameMatcher.Id,
                    GameMatcher.WorldPosition,
                    GameMatcher.Radius,
                    GameMatcher.LayerMask
                ));
        }
        
        public void Execute()
        {
            foreach (GameEntity entity in _ready.GetEntities(_buffer))
            {
                entity.TargetsBuffer.AddRange(TargetsInRadius(entity));

                if(!entity.isCollectingTargetsContinuously)
                    entity.isReadyToCollectTargets = false;
            }
        }

        private IEnumerable<int> TargetsInRadius(GameEntity entity)
        {
            return _physicsService
                .CircleCast(entity.WorldPosition, entity.Radius, entity.LayerMask)
                .Select(x => x.Id);
        }
    }
}