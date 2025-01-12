using EventSourcing.Common.Ambar;
using EventSourcing.Common.EventStore;
using EventSourcing.Common.Projection;
using EventSourcing.Common.Reaction;
using EventSourcing.Common.SerializedEvent;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.CookingClub.Membership.Reaction.EvaluateApplication;

[ApiController]
[Route("api/v1/cooking-club/membership/reaction")]
[Produces("application/json")]
[Consumes("application/json")]
[AmbarAuthMiddleware]
public class EvaluateApplicationReactionController : ReactionController
{
    private readonly EvaluateApplicationReactionHandler _evaluateApplicationReactionHandler;

    public EvaluateApplicationReactionController(
        PostgresTransactionalEventStore eventStore,
        MongoTransactionalProjectionOperator mongoOperator,
        Deserializer deserializer,
        ILogger<EvaluateApplicationReactionController> logger,
        EvaluateApplicationReactionHandler evaluateApplicationReactionHandler
    )
        : base(eventStore, mongoOperator, deserializer, logger)
    {
        _evaluateApplicationReactionHandler = evaluateApplicationReactionHandler;
    }

    [HttpPost("evaluate-application")]
    public IActionResult ReactWithEvaluateApplication([FromBody] AmbarHttpRequest request)
    {
        return new ContentResult
        {
            Content = ProcessReactionHttpRequest(request, _evaluateApplicationReactionHandler),
            ContentType = "application/json",
            StatusCode = 200,
        };
    }
}
