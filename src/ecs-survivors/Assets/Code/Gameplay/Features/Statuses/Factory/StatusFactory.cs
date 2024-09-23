using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Features.Statuses.Factory
{
    public class StatusFactory : IStatusFactory
    {
        private readonly IIdentifierService _identifierService;

        public StatusFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateStatus(StatusSetup setup, int producerId, int targetId)
        {
            GameEntity status;
            switch (setup.StatusTypeId)
            {
                case StatusTypeId.Poison:
                    status = CreatePoison(producerId, targetId, setup);
                    break;
                default:
                    throw new Exception($"Status with Type Id = {setup.StatusTypeId} does not exist");
            }

            status
                .With(x => x.AddDuration(setup.Duration), when: setup.Duration > 0)
                .With(x => x.AddTimeLeft(setup.Duration), when: setup.Duration > 0)
                .With(x => x.AddPeriod(setup.Period), when: setup.Period > 0)
                .With(x => x.AddTimeSinceLastTick(0), when: setup.Period > 0)
                ;

            return status; 
        }

        private GameEntity CreatePoison(int producerId, int targetId, StatusSetup setup)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddStatusTypeId(setup.StatusTypeId)
                .AddEffectValue(setup.Value)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .With(x => x.isStatus = true);

        }
    }
}