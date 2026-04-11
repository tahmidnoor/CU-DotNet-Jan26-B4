using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public class LaptopService
{
    private readonly IMongoCollection<Laptop> _laptops;

    public LaptopService(IConfiguration config)
    {
        var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
        var database = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);

        _laptops = database.GetCollection<Laptop>(
            config["DatabaseSettings:CollectionName"]);
    }

    public async Task<List<Laptop>> GetAsync() =>
        await _laptops.Find(_ => true).ToListAsync();

    public async Task CreateAsync(Laptop newLaptop) =>
        await _laptops.InsertOneAsync(newLaptop);
}