using MongoDB.Driver;
using TrainingAssignment1UsingMongo.Models;

namespace TrainingAssignment1UsingMongo.Interface
{
    public interface IMachineAssetRepository
    {
        public IMongoCollection<Machine> mongoCollection { get; }
        public List<Machine> GetAllMachines();
        public List<Asset> GetAllAssets();
        public Machine GetMachine(string machineName);
        public bool CreateMachine(Machine machine);
        public bool UpdateMachine(string machineName,Machine machine);
        public bool DeleteMachine(string machineName);
    }
}
