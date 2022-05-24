namespace EmailPrototypeServer.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


public class Email 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }

    public string? Name {get; set; }

    public string? WatchedFolder {get; set; }

    //[JsonConverter(typeof(StringEnumConverter))]
    //public Provider Provider {get; set; }

    public bool StoreAttachments {get; set; }
}