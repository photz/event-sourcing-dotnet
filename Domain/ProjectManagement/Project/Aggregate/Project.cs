namespace EventSourcing.Domain.ProjectManagement.Project.Aggregate;

public class Project : EventSourcing.Common.Aggregate.Aggregate
{
    public required string Name { get; init; }
}
