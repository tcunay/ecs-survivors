namespace Code.Infrastructure.View.Factory
{
    public interface IEntityViewFactory
    {
        EntityBehaviour CreateViewFormEntity(GameEntity entity);
        EntityBehaviour CreateViewFormEntityFromPrefab(GameEntity entity);
    }
}