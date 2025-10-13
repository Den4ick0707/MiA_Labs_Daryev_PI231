using LW_4_1_MiA_Daryev.Models;
using LW_4_1_MiA_Daryev_JSONFILES;

namespace LW_4_1_MiA_Daryev.Endpoints
{
    public static class ContractEndpoint
    {
        private static readonly string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "ContractData.json");

        public static void MapContractEndpoint(this WebApplication app)
        {
            try
            {
                // ------------------------- GET /contracts — Отримати всі контракти --------------------------
                app.MapGet("/contracts", () =>
                {
                    var contracts = JsonReaderAndWritter<ContractClass>.ReadJsonFile(jsonPath);
                    return Results.Ok(contracts);
                });

                // -------------------- GET /contracts/{id} — Отримати конкретний контракт --------------------
                app.MapGet("/contracts/{id:int}", (int id) =>
                {
                    var contracts = JsonReaderAndWritter<ContractClass>.ReadJsonFile(jsonPath);
                    var contract = contracts.FirstOrDefault(c => c.ContractId == id);
                    return contract is not null ? Results.Ok(contract) : Results.NotFound();
                });

                // --------------------------- POST /contracts — Додати новий контракт -------------------------
                app.MapPost("/contracts", (ContractClass newContract) =>
                {
                    var contracts = JsonReaderAndWritter<ContractClass>.ReadJsonFile(jsonPath);

                    // Auto-increment ContractId
                    newContract.ContractId = contracts.Count > 0 ? contracts.Max(c => c.ContractId) + 1 : 1;

                    contracts.Add(newContract);
                    JsonReaderAndWritter<ContractClass>.WriteJsonFile(jsonPath, contracts);

                    return Results.Created($"/contracts/{newContract.ContractId}", newContract);
                });

                // --------------------------- PUT /contracts/{id} — Оновити контракт --------------------------
                app.MapPut("/contracts/{id:int}", (int id, ContractClass updatedContract) =>
                {
                    var contracts = JsonReaderAndWritter<ContractClass>.ReadJsonFile(jsonPath);
                    var contract = contracts.FirstOrDefault(c => c.ContractId == id);
                    if (contract is null) return Results.NotFound();

                    // Оновлюємо властивості
                    contract.UserId = updatedContract.UserId;
                    contract.DeviceId = updatedContract.DeviceId;
                    contract.StartDate = updatedContract.StartDate;
                    contract.EndDate = updatedContract.EndDate;

                    JsonReaderAndWritter<ContractClass>.WriteJsonFile(jsonPath, contracts);
                    return Results.Ok(contract);
                });

                // ---------------------------- DELETE /contracts/{id} — Видалити контракт --------------------------
                app.MapDelete("/contracts/{id:int}", (int id) =>
                {
                    var contracts = JsonReaderAndWritter<ContractClass>.ReadJsonFile(jsonPath);
                    var contract = contracts.FirstOrDefault(c => c.ContractId == id);
                    if (contract is null) return Results.NotFound();

                    contracts.Remove(contract);
                    JsonReaderAndWritter<ContractClass>.WriteJsonFile(jsonPath, contracts);

                    return Results.NoContent();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
