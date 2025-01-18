using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.Identity.User.Command.SignUp;

[ApiController]
[Route("api/v1/geolab/identity/command")]
[Produces("application/json")]
[Consumes("application/json")]
public class SignUpCommandController : CommandController
{
    private readonly SignUpCommandHandler _signUpCommandHandler;

    public SignUpCommandController(
        PostgresTransactionalEventStore postgresTransactionalEventStore,
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<SignUpCommandController> logger,
        SignUpCommandHandler signUpCommandHandler
    )
        : base(postgresTransactionalEventStore, mongoTransactionalProjectionOperator, logger)
    {
        _signUpCommandHandler = signUpCommandHandler;
    }

    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult SignUp([FromBody] SignUpHttpRequest request)
    {
        var command = new SignUpCommand
        {
            PrimaryEmail = request.PrimaryEmail,
            Password = request.Password,
            Username = request.Username,
        };

        ProcessCommand(command, _signUpCommandHandler);
        return new OkObjectResult(new { });
    }
}
