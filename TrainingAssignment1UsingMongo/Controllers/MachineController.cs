using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TrainingAssignment1UsingMongo.Interface;
using TrainingAssignment1UsingMongo.Models;
using TrainingAssignment1UsingMongo.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrainingAssignment1UsingMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        readonly IMachineAssetRepository? machineAssetRepository;
        public MachineController(MachineAssetRepository machineAssetRepository)
        {
            this.machineAssetRepository = machineAssetRepository;
        }

        /// <summary>
        /// Retrieves a list of machines.
        /// </summary>
        /// <remarks>Returns a list of machines if successful.</remarks>
        /// <response code="200">Returns the list of machines.</response>
        /// <response code="500">If an internal server error occurs.</response>

        [HttpGet("GetMachines")]
        [ProducesResponseType(typeof(List<Machine>), 200)]
        [ProducesResponseType(500)]

        public IActionResult GetMachines()
        {
            List<Machine> machines = machineAssetRepository!.GetAllMachines();
            if (machines is null)
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
        [ProducesResponseType(typeof(Machine), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetMachine(string machineName)
        {
            Machine machine = machineAssetRepository!.GetMachine(machineName);
            Console.WriteLine(machine is null);
            if (machine is null)
            {
                return StatusCode(500);
            }
            else if (machine.machineName == string.Empty)
            {
                return NotFound($"{machineName} is Not Found");
            }
            return Ok(machine);
        }

        /// <summary>
        /// Creates a new machine.
        /// </summary>
        /// <param name="machine">The machine object to create.</param>
        /// <remarks>Returns a success message if the machine is created.</remarks>
        /// <response code="200">Returns a success message if the machine is created.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost("CreateMachine")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult CreteMachine(Machine machine)
        {
            if(machineAssetRepository!.CreateMachine(machine))
            {
                return Ok($"{machine.machineName} is created");
            }
            else
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates a machine.
        /// </summary>
        /// <param name="machineName">The name of the machine to update.</param>
        /// <param name="machine">The updated machine object.</param>
        /// <remarks>Returns a success message if the machine is updated.</remarks>
        /// <response code="200">Returns a success message if the machine is updated.</response>
        /// <response code="500">If an internal server error occurs.</response>

        [HttpPut("UpdateMachine")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult UpdateMachine(string machineName, Machine machine)
        {
            if (machineAssetRepository!.UpdateMachine(machineName, machine))
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes a machine by its name.
        /// </summary>
        /// <param name="machineName">The name of the machine to delete.</param>
        /// <remarks>Returns a success message if the machine is deleted.</remarks>
        /// <response code="200">Returns a success message if the machine is deleted.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpDelete("DeleteMachine")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult DeleteMachine(string machineName)
        {
             if (machineAssetRepository!.DeleteMachine(machineName))
                return Ok();
             else
                return StatusCode(500);
        }
    }
}
