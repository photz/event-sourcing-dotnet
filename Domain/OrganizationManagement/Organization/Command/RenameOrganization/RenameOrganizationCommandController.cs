using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using EventSourcing.Common.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.OrganizationManagement.Organization.Command.RenameOrganization;

[ApiController]
[Route("api/v1/org-management/organization/command")]
[Produces("application/json")]
[Consumes("application/json")]
public class RenameOrganizationCommandController : CommandController
{
    private readonly RenameOrganizationCommandHandler _renameOrganizationCommandHandler;
    private readonly IUserContext _userContext;

    public RenameOrganizationCommandController(
        PostgresTransactionalEventStore postgresTransactionalEventStore,
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<RenameOrganizationCommandController> logger,
        RenameOrganizationCommandHandler renameOrganizationCommandHandler,
        IUserContext userContext
    )
        : base(postgresTransactionalEventStore, mongoTransactionalProjectionOperator, logger)
    {
        _renameOrganizationCommandHandler = renameOrganizationCommandHandler;
        _userContext = userContext;
    }

    [HttpPost("rename-organization")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult RenameOrganization([FromBody] RenameOrganizationHttpRequest request)
    {
        var command = new RenameOrganizationCommand { Name = request.Name, OrgId = request.OrgId };

        ProcessCommand(command, _renameOrganizationCommandHandler);
        return new OkObjectResult(new { });
    }
}
