using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.OrganizationManagement.OrgMembership.Command.AddMember;

[ApiController]
[Route("api/v1/org-management/org-membership/command")]
[Produces("application/json")]
[Consumes("application/json")]
public class AddMemberCommandController : CommandController
{
    private readonly AddMemberCommandHandler _addMemberCommandHandler;

    public AddMemberCommandController(
        PostgresTransactionalEventStore postgresTransactionalEventStore,
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<AddMemberCommandController> logger,
        AddMemberCommandHandler addMemberCommandHandler
    )
        : base(postgresTransactionalEventStore, mongoTransactionalProjectionOperator, logger)
    {
        _addMemberCommandHandler = addMemberCommandHandler;
    }

    [HttpPost("add-member")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult AddMember([FromBody] AddMemberHttpRequest request)
    {
        var command = new AddMemberCommand { UserId = request.UserId, OrgId = request.OrgId };

        ProcessCommand(command, _addMemberCommandHandler);
        return new OkObjectResult(new { });
    }
}
