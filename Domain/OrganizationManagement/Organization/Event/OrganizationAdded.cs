using EventSourcing.Common.Event;
using EventSourcing.Domain.OrganizationManagement.Organization.Aggregate;

namespace EventSourcing.Domain.OrganizationManagement.Organization.Event;

public class OrganizationAdded : CreationEvent<Aggregate.Organization>
{
    public required string Name { get; init; }

    public override Aggregate.Organization CreateAggregate()
    {
        return new Aggregate.Organization()
        {
            AggregateId = AggregateId,
            AggregateVersion = AggregateVersion,
            Name = Name,
        };
    }
}
