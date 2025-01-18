using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.ProjectManagement.Project.Command.ArchiveProject;

[ApiController]
[Route("api/v1/project-management/project/command")]
[Produces("application/json")]
[Consumes("application/json")]
public class ArchiveProjectCommandController : CommandController
{
    private readonly ArchiveProjectCommandHandler _archiveProjectCommandHandler;

    public ArchiveProjectCommandController(
        PostgresTransactionalEventStore postgresTransactionalEventStore,
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<ArchiveProjectCommandController> logger,
        ArchiveProjectCommandHandler archiveProjectCommandHandler
    )
        : base(postgresTransactionalEventStore, mongoTransactionalProjectionOperator, logger)
    {
        _archiveProjectCommandHandler = archiveProjectCommandHandler;
    }

    [HttpPost("archive-project")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult ArchiveProject([FromBody] ArchiveProjectHttpRequest request)
    {
        var command = new ArchiveProjectCommand { };

        ProcessCommand(command, _archiveProjectCommandHandler);
        return new OkObjectResult(new { });
    }
}
