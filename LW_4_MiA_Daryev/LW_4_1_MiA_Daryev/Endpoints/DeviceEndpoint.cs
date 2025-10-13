using LW_4_1_MiA_Daryev.Models;
using LW_4_1_MiA_Daryev_JSONFILES;

namespace LW_4_1_MiA_Daryev.Endpoints
{
    public static class DeviceEndpoint
    {
        private static readonly string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "DeviceData.json");

        public static void MapDeviceEndpoint(this WebApplication app)
        {
            try
            {
                // ------------------------- GET /devices — Отримати всі девайси --------------------------
                app.MapGet("/devices", () =>
                {
                    var devices = JsonReaderAndWritter<DeviceClass>.ReadJsonFile(jsonPath);
                    return Results.Ok(devices);
                });

                // -------------------- GET /devices/{id} — Отримати конкретний девайс --------------------
                app.MapGet("/devices/{id:int}", (int id) =>
                {
                    var devices = JsonReaderAndWritter<DeviceClass>.ReadJsonFile(jsonPath);
                    var device = devices.FirstOrDefault(d => d.DeviceId == id);
                    return device is not null ? Results.Ok(device) : Results.NotFound();
                });

                // --------------------------- POST /devices — Додати новий девайс -------------------------
                app.MapPost("/devices", (DeviceClass newDevice) =>
                {
                    var devices = JsonReaderAndWritter<DeviceClass>.ReadJsonFile(jsonPath);

                    // Auto-increment DeviceId
                    newDevice.DeviceId = devices.Count > 0 ? devices.Max(d => d.DeviceId) + 1 : 1;

                    devices.Add(newDevice);
                    JsonReaderAndWritter<DeviceClass>.WriteJsonFile(jsonPath, devices);

                    return Results.Created($"/devices/{newDevice.DeviceId}", newDevice);
                });

                // --------------------------- PUT /devices/{id} — Оновити девайс --------------------------
                app.MapPut("/devices/{id:int}", (int id, DeviceClass updatedDevice) =>
                {
                    var devices = JsonReaderAndWritter<DeviceClass>.ReadJsonFile(jsonPath);
                    var device = devices.FirstOrDefault(d => d.DeviceId == id);
                    if (device is null) return Results.NotFound();

                    // Оновлюємо властивості
                    device.DeviceName = updatedDevice.DeviceName;
                    device.DeviceType = updatedDevice.DeviceType;
                    device.LandlordId = updatedDevice.LandlordId;

                    JsonReaderAndWritter<DeviceClass>.WriteJsonFile(jsonPath, devices);
                    return Results.Ok(device);
                });

                // ---------------------------- DELETE /devices/{id} — Видалити девайс --------------------------
                app.MapDelete("/devices/{id:int}", (int id) =>
                {
                    var devices = JsonReaderAndWritter<DeviceClass>.ReadJsonFile(jsonPath);
                    var device = devices.FirstOrDefault(d => d.DeviceId == id);
                    if (device is null) return Results.NotFound();

                    devices.Remove(device);
                    JsonReaderAndWritter<DeviceClass>.WriteJsonFile(jsonPath, devices);

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
