using EventSourcing.Common.Event;
using EventSourcing.Domain.CookingClub.Membership.Aggregate;

namespace EventSourcing.Domain.CookingClub.Membership.Event;

public class ApplicationSubmitted : CreationEvent<Aggregate.Membership>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string FavoriteCuisine { get; init; }
    public required int YearsOfProfessionalExperience { get; init; }
    public required int NumberOfCookingBooksRead { get; init; }

    public override Aggregate.Membership CreateAggregate()
    {
        return new Aggregate.Membership()
        {
            AggregateId = AggregateId,
            AggregateVersion = AggregateVersion,
            FirstName = FirstName,
            LastName = LastName,
            Status = MembershipStatus.Requested,
        };
    }
}
