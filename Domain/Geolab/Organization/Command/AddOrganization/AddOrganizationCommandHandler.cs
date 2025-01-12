using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Util;
using EventSourcing.Domain.Geolab.Organization.Event;

namespace EventSourcing.Domain.Geolab.Organization.Command.AddOrganization;

public class AddOrganizationCommandHandler : CommandHandler
{
    public AddOrganizationCommandHandler(
        PostgresTransactionalEventStore postgresTransactionalEventStore
    )
        : base(postgresTransactionalEventStore) { }

    public override void HandleCommand(Common.Command.Command command)
    {
        if (command is AddOrganizationCommand addOrganizationCommand)
        {
            HandleAddOrganization(addOrganizationCommand);
        }
        else
        {
            throw new ArgumentException($"Unsupported command type: {command.GetType().Name}");
        }
    }

    private void HandleAddOrganization(AddOrganizationCommand command)
    {
        var eventId = IdGenerator.GenerateRandomId();
        var aggregateId = IdGenerator.GenerateRandomId();

        var organizationAdded = new OrganizationAdded()
        {
            EventId = eventId,
            AggregateId = aggregateId,
            AggregateVersion = 1,
            CorrelationId = eventId,
            CausationId = eventId,
            RecordedOn = DateTime.UtcNow,
            Name = command.Name,
        };

        _postgresTransactionalEventStore.SaveEvent(organizationAdded);
    }
}
