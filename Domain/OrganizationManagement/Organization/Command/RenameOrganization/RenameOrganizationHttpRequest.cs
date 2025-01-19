using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Domain.OrganizationManagement.Organization.Command.RenameOrganization;

public class RenameOrganizationHttpRequest
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string OrgId { get; init; }
}
