namespace EventSourcing.Domain.Geolab.Project.Command.StartProject;

public class StartProjectCommand : Common.Command.Command
{
    public required string Name { get; init; }
}
