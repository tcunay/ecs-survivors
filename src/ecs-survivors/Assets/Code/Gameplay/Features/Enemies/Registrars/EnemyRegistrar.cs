using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Enemies.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Registrars
{
    public class EnemyRegistrar : MonoBehaviour
    {
        public float Speed = 1;
        public EnemyAnimator EnemyAnimator;

        private GameEntity _entity;

        private void Awake()
        {
            _entity = CreateEntity
                    .Empty()
                    .AddWorldPosition(transform.position)
                    .AddTransform(transform)
                    .AddSpeed(Speed)
                    .AddDirection(Vector2.zero)
                    .AddEnemyAnimator(EnemyAnimator)
                    .AddSpriteRenderer(EnemyAnimator.SpriteRenderer)
                    .With(x => x.isFollowingToHero = true)
                    .With(x => x.isEnemy = true)
                    .With(x => x.isTurnedAlongDirection = true)
                ;
        }
    }
}