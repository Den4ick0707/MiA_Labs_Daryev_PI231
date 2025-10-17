using LW_4_2_Daryev_MiA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LW_4_2_Daryev_MiA.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class LandlordController : Controller
    {
        public string DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSource", "LandlordData.json");
        [HttpGet]
        public IActionResult GetLandlords()
        {
            return Ok(JsonReaderAndWriter<LandlordClass>.ReadJsonFile(DataPath));
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var landlord = JsonReaderAndWriter<LandlordClass>.ReadJsonFile(DataPath).FirstOrDefault(u => u.Id.Equals(id));
            return landlord == null ? NotFound() : Ok(landlord);
        }
        [HttpPost]
        public ActionResult<LandlordClass> AddLandlord(LandlordClass landlord)
        {
            var landlords = JsonReaderAndWriter<LandlordClass>.ReadJsonFile(DataPath);
            if (landlords.Any(u => u.Id == landlord.Id))
                return BadRequest($"Landlord with ID {landlord.Id} already exists.");
            landlords.Add(landlord);
            JsonReaderAndWriter<LandlordClass>.WriteJsonFile(DataPath, landlords);
            return CreatedAtAction(nameof(GetByID), new { id = landlord.Id }, landlord);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateLandlord(int id, LandlordClass updatedLandlord)
        {
            var landlords = JsonReaderAndWriter<LandlordClass>.ReadJsonFile(DataPath);
            var landlordIndex = landlords.FindIndex(u => u.Id == id);
            if (landlordIndex == -1)
                return NotFound($"Landlord with ID {id} not found.");
            landlords[landlordIndex] = updatedLandlord;
            JsonReaderAndWriter<LandlordClass>.WriteJsonFile(DataPath, landlords);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLandlord(int id)
        {
            var landlords = JsonReaderAndWriter<LandlordClass>.ReadJsonFile(DataPath);
            var landlord = landlords.FirstOrDefault(u => u.Id == id);
            if (landlord == null)
                return NotFound($"Landlord with ID {id} not found.");
            landlords.Remove(landlord);
            JsonReaderAndWriter<LandlordClass>.WriteJsonFile(DataPath, landlords);
            return NoContent();
        }
    }
}
