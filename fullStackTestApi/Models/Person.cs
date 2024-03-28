using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fullStackTestApi.Models;

public class Person
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string PersonName { get; set; } = null!;

    public string? Quote { get; set; }
}