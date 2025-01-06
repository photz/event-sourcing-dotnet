using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Common.Util;

[ApiController]
public class DockerHealthcheckEndpoint : ControllerBase {
    [HttpGet("docker_healthcheck")]
    [HttpHead("docker_healthcheck")]
    [Produces("text/plain")] 
    public IActionResult DockerHealthCheck() {
        return Ok("OK");
    }
    [HttpGet("")]
    [HttpHead("")]
    [Produces("text/plain")] 
    public IActionResult RootDockerHealthCheck() {
        return Ok("OK");
    }
}