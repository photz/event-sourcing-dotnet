namespace EventSourcing.Domain.Identity.User.Aggregate;

public class User : EventSourcing.Common.Aggregate.Aggregate
{
    public string HashedPassword { get; init; }
    public required string PrimaryEmail { get; init; }
    public required string Username { get; init; }
}
