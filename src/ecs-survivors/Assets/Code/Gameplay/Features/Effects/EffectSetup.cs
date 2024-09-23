using System;

namespace Code.Gameplay.Features.Effects
{
    [Serializable]
    public class EffectSetup
    {
        public EffectTypeId EffectTypeId;
        public float Value;

        public EffectSetup()
        {
        }

        public EffectSetup(EffectTypeId effectTypeId, float value)
        {
            EffectTypeId = effectTypeId;
            Value = value;
        }
    }
}