using Code.Gameplay.Common;
using Code.Gameplay.Features.Enemies.Behaviours;
using Entitas;

namespace Code.Gameplay.Features.Enemies
{
    [Game] public class Enemy : IComponent { }
    [Game] public class FollowingToHero : IComponent { }
    [Game] public class EnemyAnimatorComponent : IComponent { public EnemyAnimator Value; }
}