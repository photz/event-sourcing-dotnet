using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Util;
using EventSourcing.Domain.OrganizationManagement.Organization.Event;

namespace EventSourcing.Domain.OrganizationManagement.Organization.Command.RenameOrganization;

public class RenameOrganizationCommandHandler : CommandHandler
{
    public RenameOrganizationCommandHandler(
        PostgresTransactionalEventStore postgresTransactionalEventStore
    )
        : base(postgresTransactionalEventStore) { }

    public override void HandleCommand(Common.Command.Command command)
    {
        if (command is RenameOrganizationCommand renameOrganizationCommand)
        {
            HandleRenameOrganization(renameOrganizationCommand);
        }
        else
        {
            throw new ArgumentException($"Unsupported command type: {command.GetType().Name}");
        }
    }

    private void HandleRenameOrganization(RenameOrganizationCommand command)
    {
        var eventId = IdGenerator.GenerateRandomId();
        var aggregateId = command.OrgId;

        var aggregateAndEventIds =
            _postgresTransactionalEventStore.FindAggregate<Aggregate.Organization>(command.OrgId);

        var org = aggregateAndEventIds.Aggregate;

        var organizationRenamed = new OrganizationRenamed()
        {
            EventId = eventId,
            AggregateId = aggregateId,
            AggregateVersion = org.AggregateVersion + 1,
            CorrelationId = eventId,
            CausationId = eventId,
            RecordedOn = DateTime.UtcNow,
            Name = command.Name,
        };

        _postgresTransactionalEventStore.SaveEvent(organizationRenamed);
    }
}
