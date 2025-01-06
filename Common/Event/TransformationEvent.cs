namespace EventSourcing.Common.Event;

public abstract class TransformationEvent<T> : Event where T : EventSourcing.Common.Aggregate.Aggregate
{
    public abstract T TransformAggregate(T aggregate);
}