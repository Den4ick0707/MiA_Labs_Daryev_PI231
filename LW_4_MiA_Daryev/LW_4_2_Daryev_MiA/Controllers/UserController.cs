using Microsoft.AspNetCore.Mvc;
using LW_4_2_Daryev_MiA.Models;

namespace LW_4_2_Daryev_MiA.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public string DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"DataSource","UserData.json");
        /// <summary>Get all users</summary>
        /// <remarks>json format</remarks>
        /// <returns>All users</returns>
        /// <response code="200">Sucsesfull get user list</response>
        /// <response code="404">User list is empty or not found</response>
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(JsonReaderAndWriter<UserClass>.ReadJsonFile(DataPath));
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var user = JsonReaderAndWriter<UserClass>.ReadJsonFile(DataPath).FirstOrDefault(u => u.Id.Equals(id));
            return user == null ? NotFound() : Ok(user);
        }
        [HttpPost]
        public ActionResult<UserClass> AddUser(UserClass newUser)
        {
            var users = JsonReaderAndWriter<UserClass>.ReadJsonFile(DataPath);
            if (users.Any(u => u.Id == newUser.Id))
                return BadRequest($"User with ID {newUser.Id} already exists.");
            users.Add(newUser);
            JsonReaderAndWriter<UserClass>.WriteJsonFile(DataPath, users);
            return CreatedAtAction(nameof(GetByID), new { id = newUser.Id }, newUser);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserClass updatedUser)
        {
            var users = JsonReaderAndWriter<UserClass>.ReadJsonFile(DataPath);
            var userIndex = users.FindIndex(u => u.Id == id);
            if (userIndex == -1)
                return NotFound($"User with ID {id} not found.");
            users[userIndex] = updatedUser;
            JsonReaderAndWriter<UserClass>.WriteJsonFile(DataPath, users);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var users = JsonReaderAndWriter<UserClass>.ReadJsonFile(DataPath);
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");
            users.Remove(user);
            JsonReaderAndWriter<UserClass>.WriteJsonFile(DataPath, users);
            return NoContent();
        }
    }
}
