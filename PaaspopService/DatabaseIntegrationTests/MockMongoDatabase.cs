using Microsoft.Extensions.Options;
using Mongo2Go;
using MongoDB.Driver;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Persistence.Contexts;
using PaaspopService.Persistence.Repositories;
using PaaspopService.Persistence.Settings;

namespace DatabaseIntegrationTests
{
    public class MockMongoDatabase
    {
        public MongoDbRunner Runner;

        public IMongoDatabase Database;
        public IMongoClient Client;

        public string DatabaseName = "TestDatabase";
    
        public IUsersRepository UsersRepositoryMongoDb;
        public IPerformancesRepository PerformancesRepositoryMongoDb;
        public IPlacesRepository PlacesRepositoryMongoDb;

        public MockMongoDatabase()
        {
            CreateConnection();
        }

        private void CreateConnection()
        {
            Runner = MongoDbRunner.Start(singleNodeReplSet: true);

            Client = new MongoClient(Runner.ConnectionString);
            Database = Client.GetDatabase(DatabaseName);

            UsersRepositoryMongoDb = new UsersRepositoryMongoDb(new MongoDbContext(Options.Create(new MongoDbSettings
            {
                ConnectionString = Runner.ConnectionString,
                Database = DatabaseName
            })));

            PerformancesRepositoryMongoDb = new PerformancesRepositoryMongoDb(new MongoDbContext(Options.Create(new MongoDbSettings
            {
                ConnectionString = Runner.ConnectionString,
                Database = DatabaseName
            })));

            PlacesRepositoryMongoDb = new PlacesRepositoryMongoDb(new MongoDbContext(Options.Create(new MongoDbSettings
            {
                ConnectionString = Runner.ConnectionString,
                Database = DatabaseName
            })));
        }

        ~MockMongoDatabase()
        {
            Runner.Dispose();
        }
    }
}
