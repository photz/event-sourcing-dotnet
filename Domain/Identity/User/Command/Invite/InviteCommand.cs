namespace EventSourcing.Domain.Identity.User.Command.Invite;

public class InviteCommand : Common.Command.Command
{
    public required string PrimaryEmail { get; init; }
    public required string Username { get; init; }
}
