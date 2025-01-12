using System.Text.Json.Nodes;
using EventSourcing.Domain.CookingClub.Membership.Event;
using EventSourcing.Domain.Geolab.Organization.Event;

namespace EventSourcing.Common.SerializedEvent;

public class Serializer
{
    public SerializedEvent Serialize(Event.Event @event)
    {
        return new SerializedEvent
        {
            EventId = @event.EventId,
            AggregateId = @event.AggregateId,
            AggregateVersion = @event.AggregateVersion,
            CorrelationId = @event.CorrelationId,
            CausationId = @event.CausationId,
            RecordedOn = FormatDateTime(@event.RecordedOn),
            EventName = DetermineEventName(@event),
            JsonPayload = CreateJsonPayload(@event),
            JsonMetadata = "{}",
        };
    }

    private static string DetermineEventName(Event.Event @event)
    {
        return @event switch
        {
            ApplicationSubmitted => "CookingClub_Membership_ApplicationSubmitted",
            ApplicationEvaluated => "CookingClub_Membership_ApplicationEvaluated",
            OrganizationAdded => "Geolab_Organization_OrganizationAdded",
            _ => throw new ArgumentException(
                $"Unknown event type in Serializer: {@event.GetType().Name}"
            ),
        };
    }

    private static string CreateJsonPayload(Event.Event @event)
    {
        var jsonObject = new JsonObject();

        switch (@event)
        {
            case ApplicationSubmitted applicationSubmitted:
                jsonObject.Add("firstName", applicationSubmitted.FirstName);
                jsonObject.Add("lastName", applicationSubmitted.LastName);
                jsonObject.Add("favoriteCuisine", applicationSubmitted.FavoriteCuisine);
                jsonObject.Add(
                    "yearsOfProfessionalExperience",
                    applicationSubmitted.YearsOfProfessionalExperience
                );
                jsonObject.Add(
                    "numberOfCookingBooksRead",
                    applicationSubmitted.NumberOfCookingBooksRead
                );
                break;

            case ApplicationEvaluated applicationEvaluated:
                jsonObject.Add(
                    "evaluationOutcome",
                    applicationEvaluated.EvaluationOutcome.ToString()
                );
                break;

            case OrganizationAdded organizationAdded:
                jsonObject.Add("name", organizationAdded.Name);
                break;
        }

        return jsonObject.ToJsonString();
    }

    private static string FormatDateTime(DateTime dateTime)
    {
        return dateTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff UTC");
    }
}
