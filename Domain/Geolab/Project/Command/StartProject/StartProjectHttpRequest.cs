using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Domain.Geolab.Project.Command.StartProject;

public class StartProjectHttpRequest
{
    [Required]
    public required string Name { get; init; }
}
