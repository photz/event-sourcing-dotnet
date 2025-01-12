namespace EventSourcing.Domain.Geolab.Organization.Command.AddOrganization;

public class AddOrganizationCommand : Common.Command.Command
{
    public required string Name { get; init; }
}
