using MongoDB.Bson.Serialization.Attributes;

namespace EventSourcing.Domain.CookingClub.Membership.Projection.MembersByCuisine;

public class Cuisine
{
    [BsonId]
    public required string Id { get; init; }
    public required List<string> MemberNames { get; init; }
}
