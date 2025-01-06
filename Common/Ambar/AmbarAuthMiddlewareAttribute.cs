using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventSourcing.Common.Ambar;

public class AmbarAuthMiddlewareAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var username = Environment.GetEnvironmentVariable("AMBAR_HTTP_USERNAME");
        var password = Environment.GetEnvironmentVariable("AMBAR_HTTP_PASSWORD");

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(username), 
                "Environment variables AMBAR_HTTP_USERNAME and AMBAR_HTTP_PASSWORD must be set");
        }

        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            context.Result = new JsonResult(new { error = "Authentication required" }) 
                { StatusCode = 401 };
            return;
        }

        var header = authHeader.ToString();
        if (!header.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new JsonResult(new { error = "Basic authentication required" }) 
                { StatusCode = 401 };
            return;
        }

        try
        {
            var base64Credentials = header["Basic ".Length..].Trim();
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(base64Credentials));
            var parts = credentials.Split(':', 2);

            if (parts.Length != 2)
            {
                throw new FormatException("Invalid credential format");
            }

            if (parts[0] != username || parts[1] != password)
            {
                context.Result = new JsonResult(new { error = "Invalid credentials" }) 
                    { StatusCode = 401 };
                return;
            }
        }
        catch (Exception)
        {
            context.Result = new JsonResult(new { error = "Invalid authentication format" }) 
                { StatusCode = 401 };
            return;
        }

        base.OnActionExecuting(context);
    }
}