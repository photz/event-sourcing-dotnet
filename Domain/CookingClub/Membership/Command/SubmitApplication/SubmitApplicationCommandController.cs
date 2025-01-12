using EventSourcing.Common.Command;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.CookingClub.Membership.Command.SubmitApplication;

[ApiController]
[Route("api/v1/cooking-club/membership/command")]
[Produces("application/json")]
[Consumes("application/json")]
public class SubmitApplicationCommandController : CommandController
{
    private readonly SubmitApplicationCommandHandler _submitApplicationCommandHandler;

    public SubmitApplicationCommandController(
        PostgresTransactionalEventStore postgresTransactionalEventStore,
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<SubmitApplicationCommandController> logger,
        SubmitApplicationCommandHandler submitApplicationCommandHandler
    )
        : base(postgresTransactionalEventStore, mongoTransactionalProjectionOperator, logger)
    {
        _submitApplicationCommandHandler = submitApplicationCommandHandler;
    }

    [HttpPost("submit-application")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult SubmitApplication([FromBody] SubmitApplicationHttpRequest request)
    {
        var command = new SubmitApplicationCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            FavoriteCuisine = request.FavoriteCuisine,
            YearsOfProfessionalExperience = request.YearsOfProfessionalExperience,
            NumberOfCookingBooksRead = request.NumberOfCookingBooksRead,
        };

        ProcessCommand(command, _submitApplicationCommandHandler);
        return new OkObjectResult(new { });
    }
}
