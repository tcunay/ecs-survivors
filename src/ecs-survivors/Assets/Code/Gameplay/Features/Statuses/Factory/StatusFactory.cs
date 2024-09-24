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
            GameEntity status = CreateByType(setup, producerId, targetId);

            status
                .With(x => x.AddDuration(setup.Duration), when: setup.Duration > 0)
                .With(x => x.AddTimeLeft(setup.Duration), when: setup.Duration > 0)
                .With(x => x.AddPeriod(setup.Period), when: setup.Period > 0)
                .With(x => x.AddTimeSinceLastTick(0), when: setup.Period > 0)
                ;

            return status; 
        }

        private GameEntity CreateByType(StatusSetup setup, int producerId, int targetId)
        {
            switch (setup.StatusTypeId)
            {
                case StatusTypeId.Poison:
                    return CreatePoison(producerId, targetId, setup);

                case StatusTypeId.Freeze:
                    return CreateFreeze(producerId, targetId, setup);

                case StatusTypeId.Unknown:
                default:
                    throw new Exception($"Status with Type Id = {setup.StatusTypeId} does not exist");
            }
        }

        private GameEntity CreatePoison(int producerId, int targetId, StatusSetup setup)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddStatusTypeId(setup.StatusTypeId)
                .AddEffectValue(setup.Value)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .With(x => x.isStatus = true)
                .With(x => x.isPoison = true);

        }
        
        private GameEntity CreateFreeze(int producerId, int targetId, StatusSetup setup)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddStatusTypeId(setup.StatusTypeId)
                .AddEffectValue(setup.Value)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .With(x => x.isStatus = true)
                .With(x => x.isFreeze = true);

        }
    }
}