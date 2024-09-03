using Code.Gameplay;
using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Input.Service;
using Code.Infrastructure.Systems;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class EcsRunner : MonoBehaviour
    {
        private GameContext _gameContext;
        private ITimeService _timeService;
        private IInputService _inputService;

        private BattleFeature _battleFeature;
        private ICameraProvider _cameraProvider;
        private ISystemFactory _systemsFactory;

        [Inject]
        private void Construct(ISystemFactory systemFactory)
        {
            _systemsFactory = systemFactory;
        }

        private void Start()
        {
            _battleFeature = _systemsFactory.Create<BattleFeature>(); //new BattleFeature(_gameContext, _timeService, _inputService, _cameraProvider);
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