using EventSourcing.Common.Projection;

namespace EventSourcing.Common.Query;

public abstract class QueryHandler {
    protected readonly MongoTransactionalProjectionOperator _mongoTransactionalProjectionOperator;

    protected QueryHandler(MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator) {
        _mongoTransactionalProjectionOperator = mongoTransactionalProjectionOperator;
    }

    public abstract object HandleQuery(Query query);
}