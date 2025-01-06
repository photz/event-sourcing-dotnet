using MongoDB.Bson.Serialization.Attributes;

namespace EventSourcing.Domain.CookingClub.Membership.Projection.MembersByCuisine;

public class MembershipApplication
{
    [BsonId]
    public required string Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string FavoriteCuisine { get; init; }
}