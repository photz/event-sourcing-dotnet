using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Domain.Geolab.Organization.Command.AddOrganization;

public class AddOrganizationHttpRequest
{
    [Required]
    public required string Name { get; init; }
}
