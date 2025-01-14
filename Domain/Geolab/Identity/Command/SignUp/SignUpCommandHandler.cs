using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Util;
using EventSourcing.Domain.Geolab.Identity.Event;

namespace EventSourcing.Domain.Geolab.Identity.Command.SignUp;

public class SignUpCommandHandler : CommandHandler
{
    public SignUpCommandHandler(PostgresTransactionalEventStore postgresTransactionalEventStore)
        : base(postgresTransactionalEventStore) { }

    public override void HandleCommand(Common.Command.Command command)
    {
        if (command is SignUpCommand signUpCommand)
        {
            HandleSignUp(signUpCommand);
        }
        else
        {
            throw new ArgumentException($"Unsupported command type: {command.GetType().Name}");
        }
    }

    private void HandleSignUp(SignUpCommand command)
    {
        var eventId = IdGenerator.GenerateRandomId();
        var aggregateId = IdGenerator.GenerateRandomId();

        var signedUp = new SignedUp()
        {
            EventId = eventId,
            AggregateId = aggregateId,
            AggregateVersion = 1,
            CorrelationId = eventId,
            CausationId = eventId,
            RecordedOn = DateTime.UtcNow,
            PrimaryEmail = command.PrimaryEmail,
            HashedPassword = command.Password, // FIXME: Hash password
            Username = command.Username,
        };

        _postgresTransactionalEventStore.SaveEvent(signedUp);
    }
}
