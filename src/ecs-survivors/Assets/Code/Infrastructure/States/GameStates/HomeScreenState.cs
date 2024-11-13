using Code.Common.Extensions;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;
using Code.Meta;
using Code.Meta.UI.GoldHolder.Service;

namespace Code.Infrastructure.States.GameStates
{
  public class HomeScreenState : IState, IUpdateable
  {
    private readonly ISystemFactory _systems;
    private readonly GameContext _gameContext;
    private readonly IStorageUIService _storageUIService;
    private HomeScreenFeature _homeScreenFeature;

    public HomeScreenState(ISystemFactory systems, GameContext gameContext, IStorageUIService storageUIService)
    {
      _systems = systems;
      _gameContext = gameContext;
      _storageUIService = storageUIService;
    }
    
    public void Enter()
    {
      _homeScreenFeature = _systems.Create<HomeScreenFeature>();
      _homeScreenFeature.Initialize();
    }

    public void Update()
    {
      _homeScreenFeature.Execute();
      _homeScreenFeature.Cleanup();
    }

    public void Exit()
    {
      _storageUIService.Cleanup();
      
      _homeScreenFeature.Clear(DestructEntities);
    }
    
    private void DestructEntities()
    {
      foreach (GameEntity entity in _gameContext.GetEntities())
      {
        entity.isDestructed = true;
      }
    }
  }
}