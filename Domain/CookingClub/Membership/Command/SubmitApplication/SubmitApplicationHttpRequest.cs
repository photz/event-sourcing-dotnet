using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Domain.CookingClub.Membership.Command.SubmitApplication;

public class SubmitApplicationHttpRequest
{
    [Required]
    public required string FirstName { get; init; }

    [Required]
    public required string LastName { get; init; }

    [Required]
    public required string FavoriteCuisine { get; init; }

    [Range(0, int.MaxValue)]
    public required int YearsOfProfessionalExperience { get; init; }

    [Range(0, int.MaxValue)]
    public required int NumberOfCookingBooksRead { get; init; }
}
