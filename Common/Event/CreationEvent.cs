namespace EventSourcing.Common.Event;

public abstract class CreationEvent<T> : Event
    where T : EventSourcing.Common.Aggregate.Aggregate
{
    public abstract T CreateAggregate();
}
