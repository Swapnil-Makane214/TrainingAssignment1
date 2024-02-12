using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TrainingAssignment1BackEnd.Interface;
using TrainingAssignment1BackEnd.Models;

namespace TrainingAssignment1BackEnd.Service
{
    public class TaskRepository :  ITaskRepository
    {

        private readonly IMachineAssetRepository? MachineAssetFunctions;
        public TaskRepository(MachineAssetRepository machineAssetFunctions)
        {
            this.MachineAssetFunctions = machineAssetFunctions;
        }

       
      /*  public List<Machine> GetMachines()
        {
            return MachineAssetFunctions!.GetAllMachines();
        }
*/
        public Machine GetMachine(string machineName)
        {
            return MachineAssetFunctions!.GetMachine(machineName);
        }


        public List<string> GetMachinesUsesAsset(string assetName)
        {
            List<Machine> machines = MachineAssetFunctions!.GetAllMachines();
            List<string> machineNames= new List<string>();
            foreach (Machine machine in machines)
            {
                if(machine.assetsUsed!.Where(asset => asset.assetName == assetName).Count() > 0)
                {
                    machineNames.Add(machine.machineName!);
                }
            }
            return machineNames;
        }

        public List<string> GetMachineTypesWithLatestAssetSeries()
        {
            Dictionary<string, int>? latestSeries = new();
            if (!findLatestSeries(ref latestSeries)) return null!;
            List<string>? MachineNames = new();
            findMachines(ref latestSeries, ref MachineNames);
            return MachineNames!;
        }
        private bool findLatestSeries(ref Dictionary<string, int>? latestSeries)
        {
            var assets = MachineAssetFunctions!.GetAllAssets();
            if(assets is null)
            {
                return false;
            }
            foreach (var asset in assets)
            {
                var assetName = asset.assetName;
                var series = int.Parse(asset.series!.Substring(1));
                if (latestSeries!.ContainsKey(assetName!))
                {
                    latestSeries[assetName!] = series > latestSeries[assetName!] ? series : latestSeries[assetName!];
                }
                else
                {
                    latestSeries.Add(assetName!, series);
                }
            }
            return true;
        }


        private void findMachines(ref Dictionary<string, int>? latestSeries, ref List<string>? MachineNames)
        {
            foreach (var machine in MachineAssetFunctions!.GetAllMachines()!)
            {
                var status = true;
                if (machine.assetsUsed!.Count() > 0)
                {
                    foreach (var asset in machine.assetsUsed!)
                    {
                        if (asset.series != $"S{latestSeries![asset.assetName!]}")
                        {
                            status = !status;
                            break;
                        }
                    }
                }
                else
                {
                    status = !status;
                }
                if (status)
                {
                    MachineNames!.Add(machine.machineName!);
                }
            }
        }
    }
}
