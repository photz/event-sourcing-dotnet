using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Util;
using EventSourcing.Domain.ProjectManagement.Project.Event;

namespace EventSourcing.Domain.ProjectManagement.Project.Command.ArchiveProject;

public class ArchiveProjectCommandHandler : CommandHandler
{
    public ArchiveProjectCommandHandler(
        PostgresTransactionalEventStore postgresTransactionalEventStore
    )
        : base(postgresTransactionalEventStore) { }

    public override void HandleCommand(Common.Command.Command command)
    {
        if (command is ArchiveProjectCommand archiveProjectCommand)
        {
            HandleArchiveProject(archiveProjectCommand);
        }
        else
        {
            throw new ArgumentException($"Unsupported command type: {command.GetType().Name}");
        }
    }

    private void HandleArchiveProject(ArchiveProjectCommand command)
    {
        var eventId = IdGenerator.GenerateRandomId();
        var aggregateId = IdGenerator.GenerateRandomId();

        var projectArchived = new ProjectArchived()
        {
            EventId = eventId,
            AggregateId = aggregateId,
            AggregateVersion = 1,
            CorrelationId = eventId,
            CausationId = eventId,
            RecordedOn = DateTime.UtcNow,
        };

        _postgresTransactionalEventStore.SaveEvent(projectArchived);
    }
}
