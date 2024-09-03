using Entitas;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class UpdateTransformPositionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _transforms;

        public UpdateTransformPositionSystem(GameContext gameContext)
        {
            _transforms = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform
                ));
        }
        
        public void Execute()
        {
            foreach (GameEntity target in _transforms)
            {
                target.Transform.position = target.WorldPosition;
            }
        }
    }
}