using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.OrganizationManagement.Organization.Command.AddOrganization;

[ApiController]
[Route("api/v1/org-management/organization/command")]
[Produces("application/json")]
[Consumes("application/json")]
public class AddOrganizationCommandController : CommandController
{
    private readonly AddOrganizationCommandHandler _addOrganizationCommandHandler;

    public AddOrganizationCommandController(
        PostgresTransactionalEventStore postgresTransactionalEventStore,
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<AddOrganizationCommandController> logger,
        AddOrganizationCommandHandler addOrganizationCommandHandler
    )
        : base(postgresTransactionalEventStore, mongoTransactionalProjectionOperator, logger)
    {
        _addOrganizationCommandHandler = addOrganizationCommandHandler;
    }

    [HttpPost("add-organization")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult AddOrganization([FromBody] AddOrganizationHttpRequest request)
    {
        var command = new AddOrganizationCommand { Name = request.Name };

        ProcessCommand(command, _addOrganizationCommandHandler);
        return new OkObjectResult(new { });
    }
}
