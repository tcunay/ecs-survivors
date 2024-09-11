//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCooldownUp;

    public static Entitas.IMatcher<GameEntity> CooldownUp {
        get {
            if (_matcherCooldownUp == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CooldownUp);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCooldownUp = matcher;
            }

            return _matcherCooldownUp;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Code.Gameplay.Features.Cooldowns.CooldownUp cooldownUpComponent = new Code.Gameplay.Features.Cooldowns.CooldownUp();

    public bool isCooldownUp {
        get { return HasComponent(GameComponentsLookup.CooldownUp); }
        set {
            if (value != isCooldownUp) {
                var index = GameComponentsLookup.CooldownUp;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : cooldownUpComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}