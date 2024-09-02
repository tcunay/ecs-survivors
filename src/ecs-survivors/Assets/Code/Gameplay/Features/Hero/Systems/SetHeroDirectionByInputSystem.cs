using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class SetHeroDirectionByInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _inputs;

        public SetHeroDirectionByInputSystem(GameContext gameContext)
        {
            _heroes = gameContext.GetGroup(GameMatcher.Hero);
            _inputs = gameContext.GetGroup(GameMatcher.Input);
        }
        
        public void Execute()
        {
            foreach (GameEntity input in _inputs)
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