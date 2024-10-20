using Code.Common.Entity;
using Code.Gameplay.StaticData;

namespace Code.Gameplay.Features.LevelUp.Services
{
    public class LevelUpService : ILevelUpService
    {
        private readonly IStaticDataService _staticData;

        public float CurrentExperience { get; private set; }
        public int CurrentLevel { get; private set; }

        public LevelUpService(IStaticDataService staticData)
        {
            _staticData = staticData;
        }

        public void AddExperience(float value)
        {
            CurrentExperience += value;
            UpdateLevel();
        }

        public void UpdateLevel()
        {
            if (CurrentLevel >= _staticData.MaxLevel())
            {
                return;
            }

            float experienceForLevelUp = ExperienceForLevelUp();

            if (CurrentExperience < experienceForLevelUp)
            {
                return;
            }

            CurrentExperience -= experienceForLevelUp;
            CurrentLevel++;
            CreateEntity.Empty()
                .isLevelUp = true;
            
            UpdateLevel();
        }
        
        public float ExperienceForLevelUp()
        {
            return _staticData.ExperienceForLevel(CurrentLevel + 1);
        }
    }
}