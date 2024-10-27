using System.Collections.Generic;
using Entitas;

namespace Code.Common.Systems.Destruct.Systems
{
    public class CleanupMetaDestructedSystem : ICleanupSystem
    {
        private readonly IGroup<MetaEntity> _entities;
        private readonly List<MetaEntity> _buffer = new(16);

        public CleanupMetaDestructedSystem(MetaContext gameContext)
        {
            _entities = gameContext.GetGroup(MetaMatcher.Destructed);
        }
        
        public void Cleanup()
        {
            foreach (MetaEntity gameEntity in _entities.GetEntities(_buffer))
            {
                gameEntity.Destroy();
            }
        }
    }
}