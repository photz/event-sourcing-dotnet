using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Domain.Identity.User.Command.Invite;

public class InviteHttpRequest
{
    [Required]
    public required string Username { get; init; }

    [Required]
    public required string PrimaryEmail { get; init; }
}
