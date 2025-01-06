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
            _logger.LogInformation("Created collections");

            _logger.LogInformation("Creating indexes");
            var enrollmentCollection = database.GetCollection<object>("CookingClub_MembersByCuisine_MembershipApplication");
            var indexKeysDefinition = Builders<object>.IndexKeys.Ascending("FavoriteCuisine");
            enrollmentCollection.Indexes.CreateOne(new CreateIndexModel<object>(indexKeysDefinition));
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