using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.Geolab.Project.Command.StartProject;

[ApiController]
[Route("api/v1/geolab/project/command")]
[Produces("application/json")]
[Consumes("application/json")]
public class StartProjectCommandController : CommandController
{
    private readonly StartProjectCommandHandler _startProjectCommandHandler;

    public StartProjectCommandController(
        PostgresTransactionalEventStore postgresTransactionalEventStore,
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<StartProjectCommandController> logger,
        StartProjectCommandHandler startProjectCommandHandler
    )
        : base(postgresTransactionalEventStore, mongoTransactionalProjectionOperator, logger)
    {
        _startProjectCommandHandler = startProjectCommandHandler;
    }

    [HttpPost("start-project")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult StartProject([FromBody] StartProjectHttpRequest request)
    {
        var command = new StartProjectCommand { Name = request.Name };

        ProcessCommand(command, _startProjectCommandHandler);
        return new OkObjectResult(new { });
    }
}
