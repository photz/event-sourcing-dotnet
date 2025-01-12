using EventSourcing.Common.Projection;
using EventSourcing.Common.Query;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.CookingClub.Membership.Query.MembersByCuisine;

[ApiController]
[Route("api/v1/cooking-club/membership/query")]
[Produces("application/json")]
[Consumes("application/json")]
public class MembersByCuisineQueryController : QueryController
{
    private readonly MembersByCuisineQueryHandler _membersByCuisineQueryHandler;

    public MembersByCuisineQueryController(
        MongoTransactionalProjectionOperator mongoTransactionalProjectionOperator,
        ILogger<MembersByCuisineQueryController> logger,
        MembersByCuisineQueryHandler membersByCuisineQueryHandler
    )
        : base(mongoTransactionalProjectionOperator, logger)
    {
        _membersByCuisineQueryHandler = membersByCuisineQueryHandler;
    }

    [HttpPost("members-by-cuisine")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public IActionResult ListMembersByCuisine()
    {
        var query = new MembersByCuisineQuery();
        return new OkObjectResult(ProcessQuery(query, _membersByCuisineQueryHandler));
    }
}
