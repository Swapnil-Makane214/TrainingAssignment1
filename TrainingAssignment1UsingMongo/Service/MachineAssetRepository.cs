﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrainingAssignment1UsingMongo.Interface;
using TrainingAssignment1UsingMongo.Models;

namespace TrainingAssignment1UsingMongo.Service
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
    }
}