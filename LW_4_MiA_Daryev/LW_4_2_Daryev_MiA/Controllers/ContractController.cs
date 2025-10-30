using Microsoft.AspNetCore.Mvc;
using LW_4_2_Daryev_MiA.Models;

namespace LW_4_2_Daryev_MiA.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        public string DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSource", "ContractData.json");
        [HttpGet]
        public IActionResult GetContracts()
        {
            return Ok(JsonReaderAndWriter<ContractClass>.ReadJsonFile(DataPath));
        }
        [HttpGet("{Id}")]
        public IActionResult GetContractByID(int id)
        {
            var contracts = JsonReaderAndWriter<ContractClass>.ReadJsonFile(DataPath).FirstOrDefault(u => u.ContractId.Equals(id));
            return contracts == null ? NotFound() : Ok(contracts);
        }
        [HttpPost]
        public ActionResult<ContractClass> AddContract(ContractClass newContract)
        {
            var contracts = JsonReaderAndWriter<ContractClass>.ReadJsonFile(DataPath);
            if (contracts.Any(c => c.ContractId == newContract.ContractId))
                return BadRequest($"User with ID {newContract.ContractId} already exists.");

            contracts.Add(newContract);
            JsonReaderAndWriter<ContractClass>.WriteJsonFile(DataPath, contracts);
            return CreatedAtAction(nameof(GetContractByID), new { id = newContract.ContractId }, newContract);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateContract(int id, ContractClass updateContract)
        {
            var contracts = JsonReaderAndWriter<ContractClass>.ReadJsonFile(DataPath);
            var contractIndex = contracts.FindIndex(u => u.ContractId == id);
            if (contractIndex == -1)
                return NotFound($"User with ID {id} not found.");
            contracts[contractIndex] = updateContract;
            JsonReaderAndWriter<ContractClass>.WriteJsonFile(DataPath, contracts);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteContract(int id)
        {
            var contracts = JsonReaderAndWriter<ContractClass>.ReadJsonFile(DataPath);
            var contract = contracts.FirstOrDefault(c => c.ContractId == id);
            if (contract == null)
                return NotFound($"User with ID {id} not found.");
            contracts.Remove(contract);
            JsonReaderAndWriter<ContractClass>.WriteJsonFile(DataPath, contracts);
            return NoContent();
        }

    }
}
