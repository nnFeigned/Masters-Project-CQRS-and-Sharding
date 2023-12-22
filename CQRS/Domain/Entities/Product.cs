namespace CQRS.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; } = default!;
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<Image> Images { get; set; } = new List<Image>();
}