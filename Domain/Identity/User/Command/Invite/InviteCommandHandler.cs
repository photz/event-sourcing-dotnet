using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Util;
using EventSourcing.Domain.Identity.User.Event;

namespace EventSourcing.Domain.Identity.User.Command.Invite;

public class InviteCommandHandler : CommandHandler
{
    public InviteCommandHandler(PostgresTransactionalEventStore postgresTransactionalEventStore)
        : base(postgresTransactionalEventStore) { }

    public override void HandleCommand(Common.Command.Command command)
    {
        if (command is InviteCommand inviteCommand)
        {
            HandleInvite(inviteCommand);
        }
        else
        {
            throw new ArgumentException($"Unsupported command type: {command.GetType().Name}");
        }
    }

    private void HandleInvite(InviteCommand command)
    {
        var eventId = IdGenerator.GenerateRandomId();
        var aggregateId = IdGenerator.GenerateRandomId();

        var invited = new Invited()
        {
            EventId = eventId,
            AggregateId = aggregateId,
            AggregateVersion = 1,
            CorrelationId = eventId,
            CausationId = eventId,
            RecordedOn = DateTime.UtcNow,
            PrimaryEmail = command.PrimaryEmail,
            Username = command.Username,
        };

        _postgresTransactionalEventStore.SaveEvent(invited);
    }
}
