using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.Identity.User.Command.Invite;

[ApiController]
[Route("api/v1/identity/user/command")]
[Produces("application/json")]
[Consumes("application/json")]
public class InviteCommandController : CommandController
{
    private readonly InviteCommandHandler _inviteCommandHandler;

    public InviteCommandController(
        PostgresTransactionalEventStore postgresTransactionalEventStore,
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<InviteCommandController> logger,
        InviteCommandHandler inviteCommandHandler
    )
        : base(postgresTransactionalEventStore, mongoTransactionalProjectionOperator, logger)
    {
        _inviteCommandHandler = inviteCommandHandler;
    }

    [HttpPost("invite")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult Invite([FromBody] InviteHttpRequest request)
    {
        var command = new InviteCommand
        {
            PrimaryEmail = request.PrimaryEmail,
            Username = request.Username,
        };

        ProcessCommand(command, _inviteCommandHandler);
        return new OkObjectResult(new { });
    }
}
