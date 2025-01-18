namespace EventSourcing.Domain.ProjectManagement.Project.Command.StartProject;

public class StartProjectCommand : Common.Command.Command
{
    public required string Name { get; init; }
}
