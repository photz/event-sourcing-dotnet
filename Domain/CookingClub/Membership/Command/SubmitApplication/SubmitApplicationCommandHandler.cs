using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Util;
using EventSourcing.Domain.CookingClub.Membership.Event;

namespace EventSourcing.Domain.CookingClub.Membership.Command.SubmitApplication;

public class SubmitApplicationCommandHandler : CommandHandler
{
    public SubmitApplicationCommandHandler(
        PostgresTransactionalEventStore postgresTransactionalEventStore
    )
        : base(postgresTransactionalEventStore) { }

    public override void HandleCommand(Common.Command.Command command)
    {
        if (command is SubmitApplicationCommand submitApplicationCommand)
        {
            HandleSubmitApplication(submitApplicationCommand);
        }
        else
        {
            throw new ArgumentException($"Unsupported command type: {command.GetType().Name}");
        }
    }

    private void HandleSubmitApplication(SubmitApplicationCommand command)
    {
        var eventId = IdGenerator.GenerateRandomId();
        var aggregateId = IdGenerator.GenerateRandomId();

        var applicationSubmitted = new ApplicationSubmitted()
        {
            EventId = eventId,
            AggregateId = aggregateId,
            AggregateVersion = 1,
            CorrelationId = eventId,
            CausationId = eventId,
            RecordedOn = DateTime.UtcNow,
            FirstName = command.FirstName,
            LastName = command.LastName,
            FavoriteCuisine = command.FavoriteCuisine,
            YearsOfProfessionalExperience = command.YearsOfProfessionalExperience,
            NumberOfCookingBooksRead = command.NumberOfCookingBooksRead,
        };

        _postgresTransactionalEventStore.SaveEvent(applicationSubmitted);
    }
}
