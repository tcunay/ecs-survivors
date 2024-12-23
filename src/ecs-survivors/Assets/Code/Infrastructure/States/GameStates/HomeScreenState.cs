using Code.Common.Extensions;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;
using Code.Meta;
using Code.Meta.UI.GoldHolder.Service;
using Code.Meta.UI.Shop.Service;

namespace Code.Infrastructure.States.GameStates
{
  public class HomeScreenState : EndOfFrameExitState
  {
    private readonly ISystemFactory _systems;
    private readonly GameContext _gameContext;
    private readonly IStorageUIService _storageUIService;
    private readonly IShopUIService _shopUIService;
    private HomeScreenFeature _homeScreenFeature;

    public HomeScreenState(
      GameContext gameContext, 
      ISystemFactory systems,
      IStorageUIService storageUIService,
      IShopUIService shopUIService)
    {
      _systems = systems;
      _gameContext = gameContext;
      _storageUIService = storageUIService;
      _shopUIService = shopUIService;
    }
    
    public override void Enter()
    {
      _homeScreenFeature = _systems.Create<HomeScreenFeature>();
      _homeScreenFeature.Initialize();
    }

    protected override void OnUpdate()
    {
      _homeScreenFeature.Execute();
      _homeScreenFeature.Cleanup();
    }

    protected override void ExitOnEndOfFrame()
    {
      _storageUIService.Cleanup();
      _shopUIService.Cleanup();
      
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