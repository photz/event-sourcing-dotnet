using EventSourcing.Common.Event;
using EventSourcing.Domain.ProjectManagement.Project.Aggregate;

namespace EventSourcing.Domain.ProjectManagement.Project.Event;

public class ProjectStarted : CreationEvent<Aggregate.Project>
{
    public required string Name { get; init; }

    public override Aggregate.Project CreateAggregate()
    {
        return new Aggregate.Project()
        {
            AggregateId = AggregateId,
            AggregateVersion = AggregateVersion,
            Name = Name,
        };
    }
}
