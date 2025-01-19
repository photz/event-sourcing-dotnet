namespace EventSourcing.Common.Util;

public interface IUserContext
{
    string UserId { get; }
}

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId => _httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value;
}
