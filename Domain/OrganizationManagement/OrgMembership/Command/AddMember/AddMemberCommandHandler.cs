using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Util;
using EventSourcing.Domain.OrganizationManagement.OrgMembership.Event;

namespace EventSourcing.Domain.OrganizationManagement.OrgMembership.Command.AddMember;

public class AddMemberCommandHandler : CommandHandler
{
    public AddMemberCommandHandler(PostgresTransactionalEventStore postgresTransactionalEventStore)
        : base(postgresTransactionalEventStore) { }

    public override void HandleCommand(Common.Command.Command command)
    {
        if (command is AddMemberCommand addMemberCommand)
        {
            HandleAddMember(addMemberCommand);
        }
        else
        {
            throw new ArgumentException($"Unsupported command type: {command.GetType().Name}");
        }
    }

    private void HandleAddMember(AddMemberCommand command)
    {
        var eventId = IdGenerator.GenerateRandomId();
        var aggregateId = IdGenerator.GenerateRandomId();

        _postgresTransactionalEventStore.SaveEvent(
            new MemberAdded()
            {
                EventId = eventId,
                AggregateId = aggregateId,
                AggregateVersion = 1,
                CorrelationId = eventId,
                CausationId = eventId,
                RecordedOn = DateTime.UtcNow,
                UserId = command.UserId,
                OrgId = command.OrgId,
            }
        );
    }
}
