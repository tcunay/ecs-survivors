using UnityEngine;

namespace Code.Meta.Features.AfkGain.Configs
{
    [CreateAssetMenu(menuName = "ECS Survivors/Afk Gain Config", fileName = "AfkGainConfig", order = 0) ]
    public class AfkGainConfig : ScriptableObject
    {
        public float GoldPerSecond = 1;
    }
}