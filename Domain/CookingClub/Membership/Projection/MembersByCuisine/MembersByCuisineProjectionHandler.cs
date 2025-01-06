using EventSourcing.Common.Projection;
using EventSourcing.Domain.CookingClub.Membership.Event;
using EventSourcing.Domain.CookingClub.Membership.Aggregate;

namespace EventSourcing.Domain.CookingClub.Membership.Projection.MembersByCuisine;

public class MembersByCuisineProjectionHandler : ProjectionHandler
{
    private readonly CuisineRepository _cuisineRepository;
    private readonly MembershipApplicationRepository _membershipApplicationRepository;

    public MembersByCuisineProjectionHandler(
        CuisineRepository cuisineRepository,
        MembershipApplicationRepository membershipApplicationRepository)
    {
        _cuisineRepository = cuisineRepository;
        _membershipApplicationRepository = membershipApplicationRepository;
    }

    public override void Project(Common.Event.Event @event)
    {
        switch (@event)
        {
            case ApplicationSubmitted applicationSubmitted:
                _membershipApplicationRepository.Save(new MembershipApplication
                {
                    Id = applicationSubmitted.AggregateId,
                    FirstName = applicationSubmitted.FirstName,
                    LastName = applicationSubmitted.LastName,
                    FavoriteCuisine = applicationSubmitted.FavoriteCuisine
                });
                break;

            case ApplicationEvaluated { EvaluationOutcome: MembershipStatus.Approved } applicationEvaluated:
                var membershipApplication = _membershipApplicationRepository.FindOneById(applicationEvaluated.AggregateId)
                    ?? throw new InvalidOperationException("Membership application not found");

                var cuisine = _cuisineRepository.FindOneById(membershipApplication.FavoriteCuisine) ?? new Cuisine
                {
                    Id = membershipApplication.FavoriteCuisine,
                    MemberNames = new List<string>()
                };

                cuisine.MemberNames.Add($"{membershipApplication.FirstName} {membershipApplication.LastName}");
                _cuisineRepository.Save(cuisine);
                break;
        }
    }
}