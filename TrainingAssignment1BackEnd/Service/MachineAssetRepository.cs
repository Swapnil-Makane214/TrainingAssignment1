using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrainingAssignment1BackEnd.Interface;
using TrainingAssignment1BackEnd.Models;

namespace TrainingAssignment1BackEnd.Service
{
    public class MachineAssetRepository : IMachineAssetRepository
    {
        private readonly IMongoDatabase Database;
        public MachineAssetRepository(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.connectionString);
            Database = client.GetDatabase(options.Value.database);
        }
        public IMongoCollection<Machine> mongoCollection => Database.GetCollection<Machine>("Machine");


        public List<Machine> GetAllMachines()
        {
            try
            {
                List<Machine> machines = mongoCollection.Find(machine => true).ToList();
                return machines;
            }
            catch
            {
                return null!;
            }
        }
        public List<Asset> GetAllAssets()
        {
            try
            {
                List<Machine> machines = GetAllMachines();
                List<Asset> assets = new List<Asset>();

                foreach (var machine in machines)
                {
                    foreach (var asset in machine.assetsUsed!)
                    {
                        if(assets.Where(a=> a.assetName==asset.assetName&&a.series==asset.series).Count()==0)
                            assets.Add(asset);
                    }
                }
                return assets;
            }
            catch
            {
                return null!;
            }
        }

        
       
        public Machine GetMachine(string machineName)
        {
            try
            {
                var result = mongoCollection.Find(machine => machine.machineName == machineName);
                if (result.CountDocuments()>0)
                    return result.First();
                return new Machine() { machineName =String.Empty};
            }
            catch
            {
                return null!;
            }
        }

        public bool CreateMachine(Machine machine)
        {
            try
            {
                mongoCollection.InsertOne(machine);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateMachine(string machineName, Machine machine)
        {
            try
            {
                var filter = Builders<Machine>.Filter.Eq(machine => machine.machineName, machineName);
                /*UpdateDefinition<Machine> set = Builders<Machine>.Update.Combine();
                foreach (var property in machine.GetType().GetProperties())
                {
                    set = Builders<Machine>.Update.Set(property.Name, property.GetValue(machine));

                }

                var result=mongoCollection.UpdateOne(filter, set);*/
                mongoCollection.ReplaceOne(filter, machine);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteMachine(string machineName)
        {
            try
            {
                var filter = Builders<Machine>.Filter.Eq(machine => machine.machineName, machineName);
                mongoCollection.DeleteOne(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        


       
    }
}
