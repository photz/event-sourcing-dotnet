using MongoDB.Driver;

namespace EventSourcing.Common.Util;

public class MongoInitializer
{
    private readonly MongoSessionPool _sessionPool;
    private readonly string _databaseName;
    private readonly ILogger<MongoInitializer> _logger;

    public MongoInitializer(
        MongoSessionPool sessionPool,
        string databaseName,
        ILogger<MongoInitializer> logger)
    {
        _sessionPool = sessionPool;
        _databaseName = databaseName;
        _logger = logger;
    }

    public void Initialize()
    {
        using var session = _sessionPool.StartSession();
        var database = session.Client.GetDatabase(_databaseName);

        try
        {
            _logger.LogInformation("Creating collections");
            CreateCollectionIfNotExists(database, "CookingClub_MembersByCuisine_MembershipApplication");
            CreateCollectionIfNotExists(database, "CookingClub_MembersByCuisine_Cuisine");
            CreateCollectionIfNotExists(database, "ProjectionIdempotency_ProjectedEvent");
            _logger.LogInformation("Created collections");

            _logger.LogInformation("Creating indexes");
            var membershipApplicationCollection = database.GetCollection<object>("CookingClub_MembersByCuisine_MembershipApplication");
            membershipApplicationCollection.Indexes.CreateOne(new CreateIndexModel<object>(Builders<object>.IndexKeys.Ascending("FavoriteCuisine")));
            var projectedEventCollection = database.GetCollection<object>("ProjectionIdempotency_ProjectedEvent");
            projectedEventCollection.Indexes.CreateOne(new CreateIndexModel<object>(Builders<object>.IndexKeys.Ascending("eventId").Ascending("projectionName")));
            _logger.LogInformation("Created indexes");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error initializing MongoDB");
            throw;
        }
    }

    private void CreateCollectionIfNotExists(IMongoDatabase database, string collectionName)
    {
        try
        {
            var collections = database.ListCollectionNames().ToList();
            if (!collections.Contains(collectionName))
            {
                database.CreateCollection(collectionName);
                _logger.LogInformation("Created collection {CollectionName}", collectionName);
            }
            else
            {
                _logger.LogInformation("Collection {CollectionName} already exists", collectionName);
            }
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error creating collection {CollectionName}", collectionName);
            throw;
        }
    }
}