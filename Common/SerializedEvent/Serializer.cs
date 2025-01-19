using System.Text.Json.Nodes;
using EventSourcing.Domain.CookingClub.Membership.Event;
using EventSourcing.Domain.Identity.User.Event;
using EventSourcing.Domain.OrganizationManagement.Organization.Event;
using EventSourcing.Domain.OrganizationManagement.OrgMembership.Event;
using EventSourcing.Domain.ProjectManagement.Project.Event;
using EventSourcing.Domain.SampleManagement.Sample.Event;

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
            OrganizationAdded => "OrganizationManagement_Organization_OrganizationAdded",
            OrganizationRenamed => "OrganizationManagement_Organization_OrganizationRenamed",
            ProjectStarted => "ProjectManagement_Project_ProjectStarted",
            ProjectArchived => "ProjectManagement_Project_ProjectArchived",
            SignedUp => "Identity_User_SignedUp",
            MemberAdded => "OrganizationManagement_OrgMembership_MemberAdded",
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

            case ProjectStarted projectStarted:
                jsonObject.Add("name", projectStarted.Name);
                break;

            case ProjectArchived projectArchived:
                break;

            case SignedUp signedUp:
                jsonObject.Add("username", signedUp.Username);
                jsonObject.Add("hashedPassword", signedUp.HashedPassword);
                jsonObject.Add("primaryEmail", signedUp.PrimaryEmail);
                break;

            case Invited invited:
                jsonObject.Add("username", invited.Username);
                jsonObject.Add("primaryEmail", invited.PrimaryEmail);
                break;

            case OrganizationAdded organizationAdded:
                jsonObject.Add("name", organizationAdded.Name);
                jsonObject.Add("creatorId", organizationAdded.CreatorId);
                break;

            case MemberAdded memberAdded:
                jsonObject.Add("userId", memberAdded.UserId);
                jsonObject.Add("orgId", memberAdded.OrgId);
                jsonObject.Add("inviterId", memberAdded.InviterId);
                break;

            case Collected collected:
                jsonObject.Add("tag", collected.Tag);
                break;

            case OrganizationRenamed organizationRenamed:
                jsonObject.Add("name", organizationRenamed.Name);
                break;
        }

        return jsonObject.ToJsonString();
    }

    private static string FormatDateTime(DateTime dateTime)
    {
        return dateTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff UTC");
    }
}
