using System;
using Code.Gameplay;
using Code.Gameplay.Common.Time;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class EcsRunner : MonoBehaviour
    {
        private GameContext _gameContext;
        private ITimeService _timeService;
        
        private BattleFeature _battleFeature;

        [Inject]
        private void Construct(GameContext gameContext, ITimeService timeService)
        {
            _timeService = timeService;
            _gameContext = gameContext;
        }

        private void Start()
        {
            _battleFeature = new BattleFeature(_gameContext, _timeService);
            _battleFeature.Initialize();
        }

        private void Update()
        {
            _battleFeature.Execute();
            _battleFeature.Cleanup();
        }

        private void OnDestroy()
        {
            _battleFeature.TearDown();
        }
    }
}