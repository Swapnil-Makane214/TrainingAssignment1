using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingAssignment1UsingMongo.Interface;
using TrainingAssignment1UsingMongo.Models;
using TrainingAssignment1UsingMongo.Service;

namespace TrainingAssignment1UsingMongo.Controllers
{
    [Route("api")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository functions;

        public TaskController(TaskRepository functions)
        {
            this.functions = functions;
        }

        [HttpGet("GetMachines")]
        public IActionResult GetMachines()
        {
            List<Machine> machines = functions.GetMachines();
            if(machines is null)
            {
                return StatusCode(500);
            }
            return Ok(machines);
        }


        [HttpGet("GetMachine/{machineName}")]
        public IActionResult GetMachine(string machineName)
        {
            Machine machine = functions.GetMachine(machineName);
            Console.WriteLine(machine is null);
            if (machine is null)
            {
                return StatusCode(500);
            }
            else if(machine.machineName == string.Empty)
            {
                return NotFound($"{machineName} is Not Found");
            }
            return Ok(machine);
        }
        [HttpGet("GetMachinesUsesAsset/{assetName}")]
        public IActionResult GetMachineUsesAsset(string assetName)
        {
            var machineNames = functions.GetMachinesUsesAsset(assetName);
            if (machineNames.Count()>0)
            {
                return Ok(machineNames);
            }
            else
            {
               return NotFound($"No machine uses {assetName}");
            }
        }

        [HttpGet("MachinesWithLatestAssetSeries")]
        public IActionResult GetMachineTypesWithLatestAssetSeries()
        {
            var machines = functions.GetMachineTypesWithLatestAssetSeries();
            if(machines is null)
            {
                return StatusCode(500);
            }
            else if(machines.Count()>0)
            {
                return Ok(machines);
            }
            else
            {
                return NotFound("Not found any machine with all latest series of assets");
            }
        }
    }
}
