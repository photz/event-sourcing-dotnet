namespace EventSourcing.Domain.SampleManagement.Sample.Aggregate;

public class Sample : EventSourcing.Common.Aggregate.Aggregate
{
    public required string Tag { get; init; }
}
