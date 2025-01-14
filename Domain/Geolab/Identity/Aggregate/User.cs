namespace EventSourcing.Domain.Geolab.Identity.Aggregate;

public class User : EventSourcing.Common.Aggregate.Aggregate
{
    public required string HashedPassword { get; init; }
    public required string PrimaryEmail { get; init; }
    public required string Username { get; init; }
}