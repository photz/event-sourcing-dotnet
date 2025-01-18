namespace EventSourcing.Domain.OrganizationManagement.OrgMembership.Command.AddMember;

public class AddMemberCommand : Common.Command.Command
{
    public required string UserId { get; init; }
    public required string OrgId { get; init; }
}
