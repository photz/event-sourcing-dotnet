using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Domain.OrganizationManagement.OrgMembership.Command.AddMember;

public class AddMemberHttpRequest
{
    [Required]
    public required string UserId { get; init; }

    [Required]
    public required string OrgId { get; init; }
}
