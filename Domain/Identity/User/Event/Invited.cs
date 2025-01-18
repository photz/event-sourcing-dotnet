using EventSourcing.Common.Event;
using EventSourcing.Domain.Identity.User.Aggregate;

namespace EventSourcing.Domain.Identity.User.Event;

public class Invited : CreationEvent<Aggregate.User>
{
    public required string PrimaryEmail { get; init; }
    public required string Username { get; init; }

    public override Aggregate.User CreateAggregate()
    {
        return new Aggregate.User()
        {
            AggregateId = AggregateId,
            AggregateVersion = AggregateVersion,
            PrimaryEmail = PrimaryEmail,
            Username = Username,
        };
    }
}
