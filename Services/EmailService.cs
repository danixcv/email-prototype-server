using EmailPrototypeServer.Models;
using EmailPrototypeServer.Properties;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmailPrototypeServer.Services;

public class EmailService
{
    private readonly IMongoCollection<Email> _collection;
    private readonly ILogger<EmailService> _logger;


    public EmailService(ILogger<EmailService> logger, IOptions<DatabaseProperties> properties)
    {
        var mongoClient = new MongoClient(
            properties.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            properties.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<Email>(
            properties.Value.EmailCollectionName);
            this._logger = logger;
    }

    public List<Email> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public Email? Get(string id)
    {
        return _collection.Find(x => x.Id == id).FirstOrDefault();
    }

    public void Create(Email email)
    {
        _collection.InsertOne(email);
    }

    public void Update(string id, Email email)
    {
        _collection.ReplaceOne(x => x.Id == id, email);
    }

    public void Delete(string id)
    {
        _collection.DeleteOne(x => x.Id == id);
    }
}