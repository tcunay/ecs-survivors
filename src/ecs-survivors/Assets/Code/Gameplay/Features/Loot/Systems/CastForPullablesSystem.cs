using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CastForPullablesSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _looters;
        private readonly IPhysicsService _physicsService;
        private readonly int _layerMask = CollisionLayer.Collectable.AsMask();
        private readonly GameEntity[] _hitBuffer = new GameEntity[128];

        public CastForPullablesSystem(GameContext game, IPhysicsService physicsService)
        {
            _physicsService = physicsService;
            _looters = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.PickupRadius
                    ));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _looters)
            {
                for (int i = 0; i < LootInRadius(entity); i++)
                {
                    if (_hitBuffer[i].isPullable)
                    {
                        _hitBuffer[i].isPullable = false;
                        _hitBuffer[i].isPulling = true;
                    }
                }

                ClearBuffer();
            }
        }

        private void ClearBuffer()
        {
            for (int i = 0; i < _hitBuffer.Length; i++) 
                _hitBuffer[i] = null;
        }

        private int LootInRadius(GameEntity entity) =>
            _physicsService.CircleCastNonAlloc(
                position: entity.WorldPosition,
                radius: entity.PickupRadius,
                layerMask: _layerMask,
                hitBuffer: _hitBuffer
            );
    }
}