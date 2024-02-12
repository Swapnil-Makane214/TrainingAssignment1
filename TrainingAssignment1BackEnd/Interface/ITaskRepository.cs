using MongoDB.Driver;
using TrainingAssignment1BackEnd.Models;

namespace TrainingAssignment1BackEnd.Interface
{
    public interface ITaskRepository 
    {
       
        //public List<Machine> GetMachines();
        public Machine GetMachine(string machineName);
        public List<string> GetMachinesUsesAsset(string assetName);
        public List<string> GetMachineTypesWithLatestAssetSeries();
    }
}
