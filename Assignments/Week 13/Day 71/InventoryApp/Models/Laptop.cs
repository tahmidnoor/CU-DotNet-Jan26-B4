using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

public class Laptop
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }   // 👈 MAKE NULLABLE

    [Required]
    public string ModelName { get; set; }

    [Required]
    public string SerialNumber { get; set; }

    [Range(1, 1000000)]
    public decimal Price { get; set; }
}