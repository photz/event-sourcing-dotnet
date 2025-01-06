using EventSourcing.Common.Event;
using EventSourcing.Domain.CookingClub.Membership.Aggregate;

namespace EventSourcing.Domain.CookingClub.Membership.Event;

public class ApplicationEvaluated: TransformationEvent<Aggregate.Membership>
{
    public required MembershipStatus EvaluationOutcome { get; init; }

    public override Aggregate.Membership TransformAggregate(Aggregate.Membership aggregate)
    {
        return new Aggregate.Membership()
        {
            AggregateId = AggregateId,
            AggregateVersion = AggregateVersion,
            FirstName = aggregate.FirstName,
            LastName = aggregate.LastName,
            Status = EvaluationOutcome
        };
    }
}