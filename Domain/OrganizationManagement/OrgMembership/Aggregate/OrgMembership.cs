namespace EventSourcing.Domain.OrganizationManagement.OrgMembership.Aggregate;

public class OrgMembership : EventSourcing.Common.Aggregate.Aggregate
{
    public required string UserId { get; init; }
    public required string OrgId { get; init; }
    public required OrgMembershipStatus Status { get; init; }
}
