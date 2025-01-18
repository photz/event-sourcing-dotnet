using EventSourcing.Common.Event;
using EventSourcing.Domain.SampleManagement.Sample.Aggregate;

namespace EventSourcing.Domain.SampleManagement.Sample.Event;

public class Collected : CreationEvent<Aggregate.Sample>
{
    public required string Tag { get; init; }

    public override Aggregate.Sample CreateAggregate()
    {
        return new Aggregate.Sample()
        {
            AggregateId = AggregateId,
            AggregateVersion = AggregateVersion,
            Tag = Tag,
        };
    }
}
