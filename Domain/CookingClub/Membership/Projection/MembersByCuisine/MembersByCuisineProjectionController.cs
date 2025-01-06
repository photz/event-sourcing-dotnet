using EventSourcing.Common.Ambar;
using EventSourcing.Common.Projection;
using EventSourcing.Common.SerializedEvent;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Domain.CookingClub.Membership.Projection.MembersByCuisine;

[ApiController]
[Route("api/v1/cooking-club/membership/projection")]
[Produces("application/json")]
[Consumes("application/json")]
public class MembersByCuisineProjectionController : ProjectionController
{
    private readonly MembersByCuisineProjectionHandler _membersByCuisineProjectionHandler;

    public MembersByCuisineProjectionController(
        MongoTransactionalProjectionOperator mongoOperator,
        Deserializer deserializer,
        ILogger<MembersByCuisineProjectionController> logger,
        MembersByCuisineProjectionHandler membersByCuisineProjectionHandler)
        : base(mongoOperator, deserializer, logger)
    {
        _membersByCuisineProjectionHandler = membersByCuisineProjectionHandler;
    }

    [HttpPost("members-by-cuisine")]
    public IActionResult ProjectMembersByCuisine([FromBody] AmbarHttpRequest request)
    {
        return new ContentResult
        {
            Content = ProcessProjectionHttpRequest(
                request,
                _membersByCuisineProjectionHandler,
                "CookingClub_Membership_MembersByCuisine"),
            ContentType = "application/json",
            StatusCode = 200
        };
    }
}