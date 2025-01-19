namespace EventSourcing.Domain.OrganizationManagement.Organization.Command.RenameOrganization;

public class RenameOrganizationCommand : Common.Command.Command
{
    public required string Name { get; init; }
    public required string OrgId { get; init; }
}
