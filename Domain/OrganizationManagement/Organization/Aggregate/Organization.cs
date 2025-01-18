namespace EventSourcing.Domain.OrganizationManagement.Organization.Aggregate;

public class Organization : EventSourcing.Common.Aggregate.Aggregate
{
    public required string Name { get; init; }
}
