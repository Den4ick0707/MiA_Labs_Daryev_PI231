using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LW_4_2_Daryev_MiA;
using LW_4_2_Daryev_MiA.Models;

namespace LW_4_2_Daryev_MiA.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        public string DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSource", "DeviceData.json");
        /// <summary>Get all device</summary>
        /// <remarks>Json format</remarks>
        /// <returns>All devices</returns>
        /// <response code="200">Sucsesfull get device list</response>
        /// <response code="404">Device list is empty or not found</response>
        [HttpGet]
        public IActionResult GetDevices()
        {
            return Ok(JsonReaderAndWriter<Device>.ReadJsonFile(DataPath));
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var device = JsonReaderAndWriter<Device>.ReadJsonFile(DataPath).FirstOrDefault(u => u.Id.Equals(id));
            return device == null ? NotFound() : Ok(device);
        }
        /// <summary>Post device</summary>
        /// <remarks>Enum: 
        /// PersonalComputer, 
        /// Console, 
        /// PortativeConsole </remarks>
        [HttpPost]
        public ActionResult<Device> AddDevice(Device device)
        {
            var devices = JsonReaderAndWriter<Device>.ReadJsonFile(DataPath);
            if (devices.Any(u => u.Id == device.Id))
                return BadRequest($"Device with ID {device.Id} already exists.");
            devices.Add(device);
            JsonReaderAndWriter<Device>.WriteJsonFile(DataPath, devices);
            return CreatedAtAction(nameof(GetByID), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDevice(int id, Device updatedDevice)
        {
            var devices = JsonReaderAndWriter<Device>.ReadJsonFile(DataPath);
            var deviceIndex = devices.FindIndex(u => u.Id == id);
            if (deviceIndex == -1)
                return NotFound($"Device with ID {id} not found.");
            devices[deviceIndex] = updatedDevice;
            JsonReaderAndWriter<Device>.WriteJsonFile(DataPath, devices);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDevice(int id)
        {
            var devices = JsonReaderAndWriter<Device>.ReadJsonFile(DataPath);
            var device = devices.FirstOrDefault(u => u.Id == id);
            if (device == null)
                return NotFound($"Device with ID {id} not found.");
            devices.Remove(device);
            JsonReaderAndWriter<Device>.WriteJsonFile(DataPath, devices);
            return NoContent();
        }

    }
}
