namespace EventSourcing.Domain.Geolab.Identity.Command.SignUp;

public class SignUpCommand : Common.Command.Command
{
    public required string PrimaryEmail { get; init; }
    public required string Password { get; init; }
    public required string Username { get; init; }
}
