using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.SampleManagement.Sample.Command.Collect;

[ApiController]
[Route("api/v1/sample-management/sample/collect")]
[Produces("application/json")]
[Consumes("application/json")]
public class CollectCommandController : CommandController
{
    private readonly CollectCommandHandler _collectCommandHandler;

    public CollectCommandController(
        PostgresTransactionalEventStore postgresTransactionalEventStore,
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<CollectCommandController> logger,
        CollectCommandHandler collectCommandHandler
    )
        : base(postgresTransactionalEventStore, mongoTransactionalProjectionOperator, logger)
    {
        _collectCommandHandler = collectCommandHandler;
    }

    [HttpPost("collect")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult Collect([FromBody] CollectHttpRequest request)
    {
        var command = new CollectCommand { Tag = request.Tag };

        ProcessCommand(command, _collectCommandHandler);
        return new OkObjectResult(new { });
    }
}
