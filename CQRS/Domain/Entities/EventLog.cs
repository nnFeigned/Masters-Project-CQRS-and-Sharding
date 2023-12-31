namespace CQRS.Domain.Entities;

public class EventLog : BaseEntity
{
    public Guid EntityId { get; set; }
    public required string EntityType { get; set; }

    public required string ActionType { get; set; }

    public DateTime Timestamp { get; set; }
    public bool Processed { get; set; }
}