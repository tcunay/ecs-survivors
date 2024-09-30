using Code.Gameplay.Features.Statuses.Applier;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class AddSpeedUpStatusOnEnemyDeathSystem : IExecuteSystem
    {
        private readonly IStatusApplier _statusApplier;
        private readonly IGroup<GameEntity> _enemies;
        private readonly StatusSetup _setup = new()
        {
            StatusTypeId = StatusTypeId.SpeedUp,
            Duration = 1,
            Value = 3,
        };

        public AddSpeedUpStatusOnEnemyDeathSystem(GameContext game, IStatusApplier statusApplier)
        {
            _statusApplier = statusApplier;
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.ProcessingDeath,
                    GameMatcher.Id
                ));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _enemies)
            {
                _statusApplier.ApplyStatus(_setup, entity.Id, 2);
            }
        }
    }
}