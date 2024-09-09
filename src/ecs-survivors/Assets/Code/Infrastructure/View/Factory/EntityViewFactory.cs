using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.View.Factory
{
    public class EntityViewFactory : IEntityViewFactory
    {
        private readonly Vector3 _farAway = new(-999, -999, 0);

        private readonly IAssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;

        public EntityViewFactory(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public EntityBehaviour CreateViewFormEntity(GameEntity entity)
        {
            EntityBehaviour viewPrefab = _assetProvider.LoadAsset<EntityBehaviour>(entity.ViewPath);
            EntityBehaviour view = _instantiator
                .InstantiatePrefabForComponent<EntityBehaviour>(
                    viewPrefab,
                    position: _farAway,
                    rotation: Quaternion.identity, null);

            view.SetEntity(entity);

            return view;
        }

        public EntityBehaviour CreateViewFormEntityFromPrefab(GameEntity entity)
        {
            EntityBehaviour view = _instantiator
                .InstantiatePrefabForComponent<EntityBehaviour>(
                    entity.ViewPrefab,
                    position: _farAway,
                    rotation: Quaternion.identity, null);

            view.SetEntity(entity);

            return view;
        }
    }
}