using EventSourcing.Common.Event;
using EventSourcing.Domain.OrganizationManagement.OrgMembership.Aggregate;

namespace EventSourcing.Domain.OrganizationManagement.OrgMembership.Event;

public class MemberAdded : CreationEvent<Aggregate.OrgMembership>
{
    public required string UserId { get; init; }
    public required string OrgId { get; init; }

    public override Aggregate.OrgMembership CreateAggregate()
    {
        return new Aggregate.OrgMembership()
        {
            AggregateId = AggregateId,
            AggregateVersion = AggregateVersion,
            UserId = UserId,
            OrgId = OrgId,
            Status = OrgMembershipStatus.Active,
        };
    }
}
