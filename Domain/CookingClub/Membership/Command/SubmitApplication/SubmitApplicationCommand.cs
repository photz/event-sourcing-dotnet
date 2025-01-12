namespace EventSourcing.Domain.CookingClub.Membership.Command.SubmitApplication;

public class SubmitApplicationCommand : Common.Command.Command
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string FavoriteCuisine { get; init; }
    public required int YearsOfProfessionalExperience { get; init; }
    public required int NumberOfCookingBooksRead { get; init; }
}
