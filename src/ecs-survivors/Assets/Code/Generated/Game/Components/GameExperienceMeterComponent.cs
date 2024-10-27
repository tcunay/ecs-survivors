//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherExperienceMeter;

    public static Entitas.IMatcher<GameEntity> ExperienceMeter {
        get {
            if (_matcherExperienceMeter == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ExperienceMeter);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherExperienceMeter = matcher;
            }

            return _matcherExperienceMeter;
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

    public Code.Gameplay.Features.LevelUp.LevelUpComponents.ExperienceMeterComponent experienceMeter { get { return (Code.Gameplay.Features.LevelUp.LevelUpComponents.ExperienceMeterComponent)GetComponent(GameComponentsLookup.ExperienceMeter); } }
    public Code.Gameplay.Features.LevelUp.Behaviours.ExperienceMeter ExperienceMeter { get { return experienceMeter.Value; } }
    public bool hasExperienceMeter { get { return HasComponent(GameComponentsLookup.ExperienceMeter); } }

    public GameEntity AddExperienceMeter(Code.Gameplay.Features.LevelUp.Behaviours.ExperienceMeter newValue) {
        var index = GameComponentsLookup.ExperienceMeter;
        var component = (Code.Gameplay.Features.LevelUp.LevelUpComponents.ExperienceMeterComponent)CreateComponent(index, typeof(Code.Gameplay.Features.LevelUp.LevelUpComponents.ExperienceMeterComponent));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceExperienceMeter(Code.Gameplay.Features.LevelUp.Behaviours.ExperienceMeter newValue) {
        var index = GameComponentsLookup.ExperienceMeter;
        var component = (Code.Gameplay.Features.LevelUp.LevelUpComponents.ExperienceMeterComponent)CreateComponent(index, typeof(Code.Gameplay.Features.LevelUp.LevelUpComponents.ExperienceMeterComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveExperienceMeter() {
        RemoveComponent(GameComponentsLookup.ExperienceMeter);
        return this;
    }
}