namespace Code.Gameplay.Features.Cooldowns
{
    public static class CoolDownEntityExtension
    {
        public static GameEntity PutCooldown(this GameEntity entity)
        {
            if (!entity.hasCooldown)
                return entity;

            entity.isCooldownUp = false;
            entity.ReplaceCooldownLeft(entity.Cooldown);
            
            return entity;
        }
        
        public static GameEntity PutCooldown(this GameEntity entity, float cooldown)
        {
            entity.isCooldownUp = false;
            entity.ReplaceCooldown(cooldown);
            entity.ReplaceCooldownLeft(entity.Cooldown);
            
            return entity;
        }
    }
}