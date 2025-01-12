using EventSourcing.Common.Projection;
using MongoDB.Driver;

namespace EventSourcing.Domain.CookingClub.Membership.Projection.MembersByCuisine;

public class CuisineRepository
{
    private readonly MongoTransactionalProjectionOperator _mongoOperator;
    private static string _collectionName = "CookingClub_MembersByCuisine_Cuisine";

    public CuisineRepository(MongoTransactionalProjectionOperator mongoOperator)
    {
        _mongoOperator = mongoOperator;
    }

    public void Save(Cuisine cuisine)
    {
        _mongoOperator.ReplaceOne(
            _collectionName,
            c => c.Id == cuisine.Id,
            cuisine,
            new ReplaceOptions { IsUpsert = true }
        );
    }

    public Cuisine? FindOneById(string id)
    {
        return _mongoOperator.Find<Cuisine>(_collectionName, c => c.Id == id).FirstOrDefault();
    }

    public IReadOnlyList<Cuisine> FindAll()
    {
        return _mongoOperator.Find<Cuisine>(_collectionName, _ => true);
    }
}
