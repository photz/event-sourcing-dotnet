using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Util;
using EventSourcing.Domain.Geolab.Project.Event;

namespace EventSourcing.Domain.Geolab.Project.Command.StartProject;

public class StartProjectCommandHandler : CommandHandler
{
    public StartProjectCommandHandler(
        PostgresTransactionalEventStore postgresTransactionalEventStore
    )
        : base(postgresTransactionalEventStore) { }

    public override void HandleCommand(Common.Command.Command command)
    {
        if (command is StartProjectCommand startProjectCommand)
        {
            HandleStartProject(startProjectCommand);
        }
        else
        {
            throw new ArgumentException($"Unsupported command type: {command.GetType().Name}");
        }
    }

    private void HandleStartProject(StartProjectCommand command)
    {
        var eventId = IdGenerator.GenerateRandomId();
        var aggregateId = IdGenerator.GenerateRandomId();

        var projectStarted = new ProjectStarted()
        {
            EventId = eventId,
            AggregateId = aggregateId,
            AggregateVersion = 1,
            CorrelationId = eventId,
            CausationId = eventId,
            RecordedOn = DateTime.UtcNow,
            Name = command.Name,
        };

        _postgresTransactionalEventStore.SaveEvent(projectStarted);
    }
}
