using EventSourcing.Common.Event;
using EventSourcing.Domain.Geolab.Identity.Aggregate;

namespace EventSourcing.Domain.Geolab.Identity.Event;

public class SignedUp : CreationEvent<Aggregate.User>
{
    public required string PrimaryEmail { get; init; }
    public required string Username { get; init; }
    public required string HashedPassword { get; init; }

    public override Aggregate.User CreateAggregate()
    {
        return new Aggregate.User()
        {
            AggregateId = AggregateId,
            AggregateVersion = AggregateVersion,
            PrimaryEmail = PrimaryEmail,
            Username = Username,
            HashedPassword = HashedPassword,
        };
    }
}
