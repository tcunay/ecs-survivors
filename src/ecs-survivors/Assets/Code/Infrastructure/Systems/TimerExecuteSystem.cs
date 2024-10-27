using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Infrastructure.Systems
{
    public abstract class TimerExecuteSystem : IExecuteSystem
    {
        private readonly float _interval;
        private readonly ITimeService _timeService;
        private float _timeToExecute;

        protected TimerExecuteSystem(float interval, ITimeService timeService)
        {
            _interval = interval;
            _timeService = timeService;
        }

        protected abstract void Execute();

        void IExecuteSystem.Execute()
        {
            _timeToExecute -= _timeService.DeltaTime;

            if (_timeToExecute > 0)
            {
                return;
            }
            
            _timeToExecute = _interval;
            
            Execute();
        }
    }
}