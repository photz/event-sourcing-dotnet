using EventSourcing.Common.Query;
using EventSourcing.Common.Projection;
using EventSourcing.Domain.CookingClub.Membership.Projection.MembersByCuisine;

namespace EventSourcing.Domain.CookingClub.Membership.Query.MembersByCuisine;

public class MembersByCuisineQueryHandler : QueryHandler
{
    private readonly CuisineRepository _cuisineRepository;

    public MembersByCuisineQueryHandler(
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        CuisineRepository cuisineRepository)
        : base(mongoTransactionalProjectionOperator)
    {
        _cuisineRepository = cuisineRepository;
    }

    public override object HandleQuery(Common.Query.Query query)
    {
        if (query is MembersByCuisineQuery)
        {
            return _cuisineRepository.FindAll();
        }

        throw new ArgumentException($"Unsupported query type: {query.GetType().Name}");
    }
}