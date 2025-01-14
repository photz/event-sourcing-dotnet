using System.Text.Json;
using System.Text.Json.Nodes;
using EventSourcing.Domain.CookingClub.Membership.Aggregate;
using EventSourcing.Domain.CookingClub.Membership.Event;
using EventSourcing.Domain.Geolab.Organization.Event;
using EventSourcing.Domain.Geolab.Project.Aggregate;
using EventSourcing.Domain.Geolab.Project.Event;

namespace EventSourcing.Common.SerializedEvent;

public class Deserializer
{
    public Event.Event Deserialize(SerializedEvent serializedEvent)
    {
        var recordedOn = ToDateTime(serializedEvent.RecordedOn);

        return serializedEvent.EventName switch
        {
            "CookingClub_Membership_ApplicationSubmitted" => new ApplicationSubmitted
            {
                EventId = serializedEvent.EventId,
                AggregateId = serializedEvent.AggregateId,
                AggregateVersion = serializedEvent.AggregateVersion,
                CorrelationId = serializedEvent.CorrelationId,
                CausationId = serializedEvent.CausationId,
                RecordedOn = recordedOn,
                FirstName = PayloadString(serializedEvent.JsonPayload, "firstName"),
                LastName = PayloadString(serializedEvent.JsonPayload, "lastName"),
                FavoriteCuisine = PayloadString(serializedEvent.JsonPayload, "favoriteCuisine"),
                YearsOfProfessionalExperience = PayloadInt(
                    serializedEvent.JsonPayload,
                    "yearsOfProfessionalExperience"
                ),
                NumberOfCookingBooksRead = PayloadInt(
                    serializedEvent.JsonPayload,
                    "numberOfCookingBooksRead"
                ),
            },
            "CookingClub_Membership_ApplicationEvaluated" => new ApplicationEvaluated
            {
                EventId = serializedEvent.EventId,
                AggregateId = serializedEvent.AggregateId,
                AggregateVersion = serializedEvent.AggregateVersion,
                CorrelationId = serializedEvent.CorrelationId,
                CausationId = serializedEvent.CausationId,
                RecordedOn = recordedOn,
                EvaluationOutcome = MembershipStatusHelper.FromString(
                    PayloadString(serializedEvent.JsonPayload, "evaluationOutcome")
                ),
            },
            "Geolab_Organization_OrganizationAdded" => new OrganizationAdded
            {
                EventId = serializedEvent.EventId,
                AggregateId = serializedEvent.AggregateId,
                AggregateVersion = serializedEvent.AggregateVersion,
                CorrelationId = serializedEvent.CorrelationId,
                CausationId = serializedEvent.CausationId,
                RecordedOn = recordedOn,
                Name = PayloadString(serializedEvent.JsonPayload, "name"),
            },
            "Geolab_Project_ProjectStarted" => new ProjectStarted
            {
                EventId = serializedEvent.EventId,
                AggregateId = serializedEvent.AggregateId,
                AggregateVersion = serializedEvent.AggregateVersion,
                CorrelationId = serializedEvent.CorrelationId,
                CausationId = serializedEvent.CausationId,
                RecordedOn = recordedOn,
                Name = PayloadString(serializedEvent.JsonPayload, "name"),
            },
            _ => throw new ArgumentException(
                $"Unknown event type in Deserializer: {serializedEvent.EventName}"
            ),
        };
    }

    private static DateTime ToDateTime(string recordedOn)
    {
        if (!recordedOn.EndsWith(" UTC"))
        {
            throw new ArgumentException($"Invalid date format: {recordedOn}");
        }

        if (
            DateTime.TryParseExact(
                recordedOn[..^4],
                "yyyy-MM-dd HH:mm:ss.ffffff",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AssumeUniversal,
                out var result
            )
        )
        {
            return DateTime.SpecifyKind(result, DateTimeKind.Utc);
        }

        throw new ArgumentException($"Invalid date format: {recordedOn}");
    }

    private static string PayloadString(string jsonString, string fieldName)
    {
        var jsonNode = JsonNode.Parse(jsonString);
        var value = jsonNode?[fieldName]?.GetValue<string>();
        if (value == null)
            throw new ArgumentException($"Required field {fieldName} is null or missing");
        return value;
    }

    private static int PayloadInt(string jsonString, string fieldName)
    {
        var jsonNode = JsonNode.Parse(jsonString);
        if (jsonNode?[fieldName] == null)
            throw new ArgumentException($"Required field {fieldName} is null or missing");
        return jsonNode[fieldName]!.GetValue<int>();
    }
}
