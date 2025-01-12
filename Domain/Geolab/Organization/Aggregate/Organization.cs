namespace EventSourcing.Domain.Geolab.Organization.Aggregate;

public class Organization : EventSourcing.Common.Aggregate.Aggregate
{
    public required string Name { get; init; }
}
