using fullStackTestApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace fullStackTestApi.Services;

public class PersonService
{
    private readonly IMongoCollection<Person> _namesCollection;
    public PersonService(IOptions<DbSettings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString
        );
        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.DatabaseName
        );
        _namesCollection = mongoDatabase.GetCollection<Person>(
            dbSettings.Value.NamesCollectionName
        );
    }

    public async Task<List<Person>> GetAsync() =>
        await _namesCollection.Find(_ => true).ToListAsync();

    public async Task<Person?> GetAsync(string id) =>
        await _namesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Person newName) =>
        await _namesCollection.InsertOneAsync(newName);

    public async Task UpdateAsync(string id, Person updatedPerson) =>
        await _namesCollection.ReplaceOneAsync(x => x.Id == id, updatedPerson);

    public async Task RemoveAsync(string id) =>
        await _namesCollection.DeleteOneAsync(x => x.Id == id);
}