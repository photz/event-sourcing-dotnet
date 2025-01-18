namespace EventSourcing.Domain.SampleManagement.Sample.Command.Collect;

public class CollectCommand : Common.Command.Command
{
    public required string Tag { get; init; }
}
