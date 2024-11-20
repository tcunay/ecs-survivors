using System.Collections.Generic;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Entitas;

namespace Code.Gameplay.Features.GameOver
{
    public class GameOverOnHeroDeathSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGameStateMachine _stateMachine;

        public GameOverOnHeroDeathSystem(GameContext game, IGameStateMachine stateMachine) : base(game)
        {
            _stateMachine = stateMachine;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.Dead)
                .Added());

        protected override bool Filter(GameEntity entity) => entity.isHero && entity.isDead;

        protected override void Execute(List<GameEntity> entities)
        {
            _stateMachine.Enter<GameOverState>();
        }
    }
}