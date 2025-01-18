using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Domain.OrganizationManagement.Organization.Command.AddOrganization;

public class AddOrganizationHttpRequest
{
    [Required]
    public required string Name { get; init; }
}
