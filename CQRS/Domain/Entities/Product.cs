﻿using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CQRS.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; } = default!;
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }

    [BsonIgnore]
    public Category Category { get; set; }

    public ICollection<Image> Images { get; set; } = new List<Image>();
}