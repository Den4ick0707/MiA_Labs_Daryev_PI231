using LW_4_1_MiA_Daryev.Models;
using LW_4_1_MiA_Daryev_JSONFILES;

namespace LW_4_1_MiA_Daryev.Endpoints
{
    public static class LandlordEndpoint
    {
        private static readonly string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "LandlordData.json");

        public static void MapLandlordEndpoint(this WebApplication app)
        {
            try
            {
                // ------------------------- GET /landlords — Отримати всіх орендодавців --------------------------
                app.MapGet("/landlords", () =>
                {
                    var landlords = JsonReaderAndWritter<Landlord>.ReadJsonFile(jsonPath);
                    return Results.Ok(landlords);
                });

                // -------------------- GET /landlords/{id} — Отримати конкретного орендодавця --------------------
                app.MapGet("/landlords/{id:int}", (int id) =>
                {
                    var landlords = JsonReaderAndWritter<Landlord>.ReadJsonFile(jsonPath);
                    var landlord = landlords.FirstOrDefault(l => l.LandlordId == id);
                    return landlord is not null ? Results.Ok(landlord) : Results.NotFound();
                });

                // --------------------------- POST /landlords — Додати нового орендодавця -------------------------
                app.MapPost("/landlords", (Landlord newLandlord) =>
                {
                    var landlords = JsonReaderAndWritter<Landlord>.ReadJsonFile(jsonPath);

                    // Auto-increment LandlordId
                    newLandlord.LandlordId = landlords.Count > 0 ? landlords.Max(l => l.LandlordId) + 1 : 1;

                    landlords.Add(newLandlord);
                    JsonReaderAndWritter<Landlord>.WriteJsonFile(jsonPath, landlords);

                    return Results.Created($"/landlords/{newLandlord.LandlordId}", newLandlord);
                });

                // --------------------------- PUT /landlords/{id} — Оновити орендодавця --------------------------
                app.MapPut("/landlords/{id:int}", (int id, Landlord updatedLandlord) =>
                {
                    var landlords = JsonReaderAndWritter<Landlord>.ReadJsonFile(jsonPath);
                    var landlord = landlords.FirstOrDefault(l => l.LandlordId == id);
                    if (landlord is null) return Results.NotFound();

                    landlord.LandlordName = updatedLandlord.LandlordName;

                    JsonReaderAndWritter<Landlord>.WriteJsonFile(jsonPath, landlords);
                    return Results.Ok(landlord);
                });

                // ---------------------------- DELETE /landlords/{id} — Видалити орендодавця --------------------------
                app.MapDelete("/landlords/{id:int}", (int id) =>
                {
                    var landlords = JsonReaderAndWritter<Landlord>.ReadJsonFile(jsonPath);
                    var landlord = landlords.FirstOrDefault(l => l.LandlordId == id);
                    if (landlord is null) return Results.NotFound();

                    landlords.Remove(landlord);
                    JsonReaderAndWritter<Landlord>.WriteJsonFile(jsonPath, landlords);

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
