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

        /// <summary>
        /// Retrieves a list of machines.
        /// </summary>
        /// <remarks>Returns a list of machines if successful.</remarks>
        /// <response code="200">Returns the list of machines.</response>
        /// <response code="500">If an internal server error occurs.</response>

        [HttpGet("GetMachines")]
        [ProducesResponseType(typeof(List<Machine>),200)]
        [ProducesResponseType(500)]

        public IActionResult GetMachines()
        {
            List<Machine> machines = functions.GetMachines();
            if(machines is null)
            {
                return StatusCode(500);
            }
            return Ok(machines);
        }
        /// <summary>
        /// Retrieves a machine by its name.
        /// </summary>
        /// <param name="machineName">The name of the machine to retrieve.</param>
        /// <remarks>Returns the machine if found.</remarks>
        /// <response code="200">Returns the machine if found.</response>
        /// <response code="404">If the machine is not found.</response>
        /// <response code="500">If an internal server error occurs.</response>  

        [HttpGet("GetMachine/{machineName}")]
        [ProducesResponseType(typeof(Machine),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Retrieves a list of machine names that use the specified asset.
        /// </summary>
        /// <param name="assetName">The name of the asset.</param>
        /// <remarks>Returns the list of machine names if any use the specified asset.</remarks>
        /// <response code="200">Returns the list of machine names which uses "assetName" asset if found.</response>
        /// <response code="404">If no machine uses the specified asset.</response>

        [HttpGet("GetMachinesUsesAsset/{assetName}")]
        [ProducesResponseType(typeof(List<string>),200)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Retrieves machine types with the latest series of assets.
        /// </summary>
        /// <remarks>Returns machine types with the latest series of assets if found.</remarks>
        /// <response code="200">Returns machine types which uses latest series of assets.</response>
        /// <response code="404">If no machine types with the latest series of assets are found.</response>
        /// <response code="500">If an internal server error occurs.</response>
        

        [HttpGet("MachinesWithLatestAssetSeries")]
        [ProducesResponseType(typeof(List<string>),200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
