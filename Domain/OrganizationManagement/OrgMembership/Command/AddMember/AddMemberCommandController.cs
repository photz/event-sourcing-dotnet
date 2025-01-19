using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using EventSourcing.Common.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.OrganizationManagement.OrgMembership.Command.AddMember;

[ApiController]
[Route("api/v1/org-management/org-membership/command")]
[Produces("application/json")]
[Consumes("application/json")]
public class AddMemberCommandController : CommandController
{
    private readonly AddMemberCommandHandler _addMemberCommandHandler;
    private readonly IUserContext _userContext;

    public AddMemberCommandController(
        PostgresTransactionalEventStore postgresTransactionalEventStore,
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<AddMemberCommandController> logger,
        AddMemberCommandHandler addMemberCommandHandler,
        IUserContext userContext
    )
        : base(postgresTransactionalEventStore, mongoTransactionalProjectionOperator, logger)
    {
        _addMemberCommandHandler = addMemberCommandHandler;

        _userContext = userContext;
    }

    [HttpPost("add-member")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult AddMember([FromBody] AddMemberHttpRequest request)
    {
        var command = new AddMemberCommand
        {
            UserId = request.UserId,
            OrgId = request.OrgId,
            InviterId = _userContext.UserId,
        };

        ProcessCommand(command, _addMemberCommandHandler);
        return new OkObjectResult(new { });
    }
}
