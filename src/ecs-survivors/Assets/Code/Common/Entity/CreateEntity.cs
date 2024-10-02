namespace Code.Common.Entity
{
  public static class CreateEntity
  {
    public static GameEntity Empty() =>
      Contexts.sharedInstance.game.CreateEntity();
    
    public static GameEntity Empty(int id) => Empty().AddId(id);
  }
}