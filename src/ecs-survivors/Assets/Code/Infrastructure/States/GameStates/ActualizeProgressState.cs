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
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
    public class ActualizeProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IProgressProvider _progressProvider;
        private readonly ISystemFactory _systemFactory;
        private readonly ITimeService _timeService;
        private readonly TimeSpan _twoDays = TimeSpan.FromDays(2);

        private ActualizationFeature _actualizationFeature;
        
        public ActualizeProgressState(
            IGameStateMachine stateMachine,
            IProgressProvider progressProvider,
            ISystemFactory systemFactory,
            ITimeService timeService
            )
        {
            _stateMachine = stateMachine;
            _progressProvider = progressProvider;
            _systemFactory = systemFactory;
            _timeService = timeService;
        }
        
        public void Enter()
        {
            _actualizationFeature = _systemFactory.Create<ActualizationFeature>();

            _progressProvider.ProgressData.LastSimulationTickTime = _timeService.UtcNow - _twoDays;
            
            ActualizeProgress(_progressProvider.ProgressData);
            
            _stateMachine.Enter<LoadingHomeScreenState>();
        }

        private void ActualizeProgress(ProgressData data)
        {
           CreateMetaEntity.Empty()
                .AddGoldGainBoost(1)
                .AddDuration((float) TimeSpan.FromDays(2).TotalSeconds);
            
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