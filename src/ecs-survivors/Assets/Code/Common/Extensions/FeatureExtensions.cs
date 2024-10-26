using System;

namespace Code.Common.Extensions
{
    public static class FeatureExtensions
    {
        public static void Clear(this Feature feature, Action destructEntities)
        {
            feature.DeactivateReactiveSystems();
            feature.ClearReactiveSystems();

            destructEntities?.Invoke();
      
            feature.Cleanup();
            feature.TearDown();
        }
    }
}