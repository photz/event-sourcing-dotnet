using EventSourcing.Common.EventStore;
using EventSourcing.Common.Reaction;
using EventSourcing.Domain.CookingClub.Membership.Aggregate;
using EventSourcing.Domain.CookingClub.Membership.Event;
using static EventSourcing.Common.Util.IdGenerator;

namespace EventSourcing.Domain.CookingClub.Membership.Reaction.EvaluateApplication;

public class EvaluateApplicationReactionHandler : ReactionHandler
{
    public EvaluateApplicationReactionHandler(PostgresTransactionalEventStore eventStore)
        : base(eventStore) { }

    public override void React(Common.Event.Event @event)
    {
        if (@event is not ApplicationSubmitted applicationSubmitted)
        {
            return;
        }

        var aggregateAndEventIds =
            _postgresTransactionalEventStore.FindAggregate<Aggregate.Membership>(
                applicationSubmitted.AggregateId
            );
        var membership = aggregateAndEventIds.Aggregate;
        var causationId = aggregateAndEventIds.EventIdOfLastEvent;
        var correlationId = aggregateAndEventIds.CorrelationIdOfLastEvent;

        if (membership.Status != MembershipStatus.Requested)
        {
            return;
        }

        var reactionEventId = GenerateDeterministicId(
            $"CookingClub_Membership_ReviewedApplication:{applicationSubmitted.EventId}"
        );
        if (_postgresTransactionalEventStore.DoesEventAlreadyExist(reactionEventId))
        {
            return;
        }

        var shouldApprove =
            applicationSubmitted
                is { YearsOfProfessionalExperience: 0, NumberOfCookingBooksRead: > 0 };

        var reactionEvent = new ApplicationEvaluated
        {
            EventId = reactionEventId,
            AggregateId = membership.AggregateId,
            AggregateVersion = membership.AggregateVersion + 1,
            CausationId = causationId,
            CorrelationId = correlationId,
            RecordedOn = DateTime.UtcNow,
            EvaluationOutcome = shouldApprove
                ? MembershipStatus.Approved
                : MembershipStatus.Rejected,
        };

        _postgresTransactionalEventStore.SaveEvent(reactionEvent);
    }
}
