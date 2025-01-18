using System.ComponentModel.DataAnnotations;

namespace EventSourcing.Domain.SampleManagement.Sample.Command.Collect;

public class CollectHttpRequest
{
    [Required]
    public required string Tag { get; init; }
}
