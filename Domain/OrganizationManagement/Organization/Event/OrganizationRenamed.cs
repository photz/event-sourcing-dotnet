using EventSourcing.Common.Event;
using EventSourcing.Domain.OrganizationManagement.Organization.Aggregate;

namespace EventSourcing.Domain.OrganizationManagement.Organization.Event;

public class OrganizationRenamed : TransformationEvent<Aggregate.Organization>
{
    public required string Name { get; init; }

    public override Aggregate.Organization TransformAggregate(Aggregate.Organization aggregate)
    {
        return new Aggregate.Organization()
        {
            AggregateId = AggregateId,
            AggregateVersion = AggregateVersion,
            Name = Name,
            CreatorId = aggregate.CreatorId,
        };
    }
}
