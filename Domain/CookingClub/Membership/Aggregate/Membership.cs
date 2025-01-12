namespace EventSourcing.Domain.CookingClub.Membership.Aggregate;

public class Membership : EventSourcing.Common.Aggregate.Aggregate
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required MembershipStatus Status { get; init; }
}
