using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Util;
using EventSourcing.Domain.SampleManagement.Sample.Event;

namespace EventSourcing.Domain.SampleManagement.Sample.Command.Collect;

public class CollectCommandHandler : CommandHandler
{
    public CollectCommandHandler(PostgresTransactionalEventStore postgresTransactionalEventStore)
        : base(postgresTransactionalEventStore) { }

    public override void HandleCommand(Common.Command.Command command)
    {
        if (command is CollectCommand collectCommand)
        {
            HandleCollect(collectCommand);
        }
        else
        {
            throw new ArgumentException($"Unsupported command type: {command.GetType().Name}");
        }
    }

    private void HandleCollect(CollectCommand command)
    {
        var eventId = IdGenerator.GenerateRandomId();
        var aggregateId = IdGenerator.GenerateRandomId();

        var collected = new Collected()
        {
            EventId = eventId,
            AggregateId = aggregateId,
            AggregateVersion = 1,
            CorrelationId = eventId,
            CausationId = eventId,
            RecordedOn = DateTime.UtcNow,
            Tag = command.Tag,
        };

        _postgresTransactionalEventStore.SaveEvent(collected);
    }
}
