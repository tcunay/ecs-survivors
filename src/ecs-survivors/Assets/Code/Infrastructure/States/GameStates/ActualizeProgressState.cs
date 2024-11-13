using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Common.Time;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Systems;
using Code.Meta;
using Code.Meta.Features.Simulation.Systems;
using Code.Progress.Data;
using Code.Progress.Provider;
using Code.Progress.SaveLoad;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
    public class ActualizeProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IProgressProvider _progressProvider;
        private readonly ISystemFactory _systemFactory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ITimeService _timeService;
        private readonly TimeSpan _twoDays = TimeSpan.FromDays(2);

        private ActualizationFeature _actualizationFeature;
        
        public ActualizeProgressState(
            IGameStateMachine stateMachine,
            IProgressProvider progressProvider,
            ISystemFactory systemFactory,
            ISaveLoadService saveLoadService,
            ITimeService timeService
            )
        {
            _stateMachine = stateMachine;
            _progressProvider = progressProvider;
            _systemFactory = systemFactory;
            _saveLoadService = saveLoadService;
            _timeService = timeService;
        }
        
        public void Enter()
        {
            _actualizationFeature = _systemFactory.Create<ActualizationFeature>();

            ActualizeProgress(_progressProvider.ProgressData);
            
            _stateMachine.Enter<LoadingHomeScreenState>();
        }

        private void ActualizeProgress(ProgressData data)
        {
            _actualizationFeature.Initialize();
            _actualizationFeature.DeactivateReactiveSystems();

            DateTime until = GetLimitedUntilTime(data);
            
            Debug.Log($"Actualize {(until - data.LastSimulationTickTime).TotalSeconds} seconds");

            while (data.LastSimulationTickTime < until)
            {
                MetaEntity tick = CreateMetaEntity
                    .Empty()
                    .AddTick(MetaConstants.SimulationTickSeconds);
                
                _actualizationFeature.Execute();
                _actualizationFeature.Cleanup();
                
                tick.Destroy();
            }
            
            data.LastSimulationTickTime = _timeService.UtcNow;
            
            _saveLoadService.SaveProgress();
        }

        private DateTime GetLimitedUntilTime(ProgressData data)
        {
            return _timeService.UtcNow - data.LastSimulationTickTime < _twoDays
                ? _timeService.UtcNow
                : data.LastSimulationTickTime + _twoDays;
        }
        
        public void Exit()
        {
            /*_actualizationFeature.Cleanup();
            _actualizationFeature.TearDown();*/
            
            
            _actualizationFeature.Clear();
            
            _actualizationFeature = null;
            
            
        }

        
    }
}