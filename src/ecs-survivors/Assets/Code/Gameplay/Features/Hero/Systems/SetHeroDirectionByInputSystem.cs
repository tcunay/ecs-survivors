using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class SetHeroDirectionByInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<InputEntity> _inputs;

        public SetHeroDirectionByInputSystem(GameContext gameContext, InputContext inputContext)
        {
            _heroes = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.MovementAvailable
                    ));
            
            _inputs = inputContext.GetGroup(InputMatcher.Input);
        }
        
        public void Execute()
        {
            foreach (InputEntity input in _inputs)
            {
                foreach (GameEntity hero in _heroes)
                {
                    hero.isMoving = input.hasAxisInput;

                    if (input.hasAxisInput)
                    {
                        hero.ReplaceDirection(input.AxisInput.normalized);
                    }
                }
            }
        }
    }
}