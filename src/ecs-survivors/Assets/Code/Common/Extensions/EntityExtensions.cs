namespace Code.Common.Extensions
{
    public static class EntityExtensions
    {
        public static GameEntity RemoveTargetCollectionComponents(this GameEntity entity)
        {
            if (entity.hasTargetsBuffer)
            {
                entity.RemoveTargetsBuffer();
            }

            if (entity.hasCollectTargetsInterval)
            {
                entity.RemoveCollectTargetsInterval();
            }

            if (entity.hasCollectTargetsTimer)
            {
                entity.RemoveCollectTargetsTimer();
            }

            return entity;
        }
    }
}