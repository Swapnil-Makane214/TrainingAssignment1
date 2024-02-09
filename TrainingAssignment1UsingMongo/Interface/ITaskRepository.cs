using MongoDB.Driver;
using TrainingAssignment1UsingMongo.Models;

namespace TrainingAssignment1UsingMongo.Interface
{
    public interface ITaskRepository 
    {
       
        public List<Machine> GetMachines();
        public List<Asset> GetAssets();
        public Machine GetMachine(string machineName);
        public List<string> GetMachinesUsesAsset(string assetName);
        public List<string> GetMachineTypesWithLatestAssetSeries();
    }
}
