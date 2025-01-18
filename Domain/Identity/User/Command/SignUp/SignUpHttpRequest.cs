using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Domain.Identity.User.Command.SignUp;

public class SignUpHttpRequest
{
    [Required]
    public required string Username { get; init; }

    [Required]
    public required string Password { get; init; }

    [Required]
    public required string PrimaryEmail { get; init; }
}
