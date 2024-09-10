using System;
using System.Collections.Generic;
using Code.Gameplay.Common.Physics;
using Entitas;

public class CastForTargetsWithLimitSystem : IExecuteSystem, ITearDownSystem
{
    private readonly IPhysicsService _physicsService;
    private readonly IGroup<GameEntity> _ready;
    private readonly List<GameEntity> _buffer = new(64);

    private GameEntity[] _targetCastBuffer = new GameEntity[128];

    public CastForTargetsWithLimitSystem(GameContext gameContext, IPhysicsService physicsService)
    {
        _physicsService = physicsService;
        _ready = gameContext.GetGroup(GameMatcher
            .AllOf(
                GameMatcher.ReadyToCollectTargets,
                GameMatcher.TargetsBuffer,
                GameMatcher.TargetLimit,
                GameMatcher.ProcessedTargets,
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
            for (int i = 0; i < Math.Min(TargetsCountInRadius(entity), entity.TargetLimit); i++)
            {
                int targetId = _targetCastBuffer[i].Id;
                
                if (!AlreadyProcessed(entity, targetId))
                {
                    entity.TargetsBuffer.Add(targetId);
                    entity.ProcessedTargets.Add(targetId);
                }
            }

            if (!entity.isCollectingTargetsContinuously)
                entity.isReadyToCollectTargets = false;
        }
    }

    private bool AlreadyProcessed(GameEntity entity, int targetId)
    {
       return entity.ProcessedTargets.Contains(targetId);
    }

    private int TargetsCountInRadius(GameEntity entity)
    {
        return _physicsService
            .CircleCastNonAlloc(entity.WorldPosition, entity.Radius, entity.LayerMask, _targetCastBuffer);
    }

    public void TearDown()
    {
        _targetCastBuffer = null;
    }
}