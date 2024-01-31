using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Driver.API;

public class DriverService
{
    private readonly IMongoCollection<Driver> _mongoCollection;

    public DriverService(IOptions<DriverConfigurationDb> options)
    {
        // Inicia a configuracao de conexao com o banco
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        // Nome do banco de dados
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        _mongoCollection = mongoDatabase.GetCollection<Driver>(options.Value.CollectionName);
    }

    //Methods 
    public async Task<List<Driver>> GetAll()
        => await _mongoCollection.Find(_ => true).ToListAsync();

    public async Task<Driver> GetById(string id)
        => await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task PostDriver(Driver driverRequest)
        => await _mongoCollection.InsertOneAsync(driverRequest);

    public async Task PutDriver(string id, Driver driverRequest)
        => await _mongoCollection.ReplaceOneAsync(x => x.Id == id, driverRequest);

    public async Task DeleteDriver(string id)
        => await _mongoCollection.DeleteOneAsync(x => x.Id == id);

}
