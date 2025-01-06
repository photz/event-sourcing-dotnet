namespace EventSourcing.Domain.CookingClub.Membership.Aggregate;

public enum MembershipStatus
{
    Requested,
    Approved,
    Rejected
}

public static class MembershipStatusHelper
{
    public static MembershipStatus FromString(string status)
    {
        if (Enum.TryParse(status, true, out MembershipStatus result))
        {
            return result;
        }
        else
        {
            throw new ArgumentException($"Invalid MembershipStatus: {status}");
        }
    }
}