using EventSourcing.Common.Event;
using EventSourcing.Domain.ProjectManagement.Project.Aggregate;

namespace EventSourcing.Domain.ProjectManagement.Project.Event;

public class ProjectArchived : TransformationEvent<Aggregate.Project>
{
    public override Aggregate.Project TransformAggregate(Aggregate.Project aggregate)
    {
        return new Aggregate.Project()
        {
            AggregateId = AggregateId,
            AggregateVersion = AggregateVersion,
            Name = aggregate.Name,
            Status = ProjectStatus.Archived,
        };
    }
}
