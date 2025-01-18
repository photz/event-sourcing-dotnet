namespace EventSourcing.Domain.OrganizationManagement.Organization.Command.AddOrganization;

public class AddOrganizationCommand : Common.Command.Command
{
    public required string Name { get; init; }
}
