using EventSourcing.Common.Projection;
using MongoDB.Driver;

namespace EventSourcing.Domain.CookingClub.Membership.Projection.MembersByCuisine;

public class MembershipApplicationRepository
{
    private readonly MongoTransactionalProjectionOperator _mongoOperator;
    private static string _collectionName = "CookingClub_MembersByCuisine_MembershipApplication";

    public MembershipApplicationRepository(MongoTransactionalProjectionOperator mongoOperator)
    {
        _mongoOperator = mongoOperator;
    }

    public void Save(MembershipApplication membershipApplication)
    {
        _mongoOperator.ReplaceOne(
            _collectionName,
            m => m.Id == membershipApplication.Id,
            membershipApplication,
            new ReplaceOptions { IsUpsert = true }
        );
    }

    public MembershipApplication? FindOneById(string id)
    {
        return _mongoOperator
            .Find<MembershipApplication>(_collectionName, m => m.Id == id)
            .FirstOrDefault();
    }
}
