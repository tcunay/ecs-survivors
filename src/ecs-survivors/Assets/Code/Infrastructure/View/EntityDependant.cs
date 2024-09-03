using System;
using UnityEngine;

namespace Code.Infrastructure.View
{
    public abstract class EntityDependant : MonoBehaviour
    {
        public EntityBehaviour EntityView;

        public GameEntity Entity => EntityView?.Entity;

        private void Awake()
        {
            if (!EntityView) 
                EntityView = GetComponent<EntityBehaviour>();
        }
    }
}