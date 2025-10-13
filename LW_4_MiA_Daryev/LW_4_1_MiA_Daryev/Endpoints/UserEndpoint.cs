using LW_4_1_MiA_Daryev.Models;
using LW_4_1_MiA_Daryev_JSONFILES;
using System.Diagnostics.Contracts;
using System.Globalization;
namespace LW_4_1_MiA_Daryev.Endpoints
{
    public static class UserEndpoint
    {
        private static readonly string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "UserData.json");

        public static void MapUserEndpoints(this WebApplication app)
        {
            Console.WriteLine(jsonPath);

            //------------------------- GET /users — Отримати всі користувачі --------------------------
            app.MapGet("/users", (int? id, string? name, string? sortBy, string? order) =>
            {
                var userItems = JsonReaderAndWritter<UserClass>.ReadJsonFile(jsonPath);

                // Users filter
                if (id.HasValue) userItems = userItems.Where(c => c.UserId == id.Value).ToList();
                if (!string.IsNullOrEmpty(name) || !string.IsNullOrWhiteSpace(name))
                    userItems = userItems.Where(c => c.UserName == name).ToList();

                // Users sort
                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    bool descending = order?.Equals("desc", StringComparison.OrdinalIgnoreCase) == true;

                    userItems = sortBy.ToLower() switch
                    {
                        "id" or "userid" => descending
                            ? userItems.OrderByDescending(u => u.UserId).ToList()
                            : userItems.OrderBy(u => u.UserId).ToList(),

                        "name" or "username" => descending
                            ? userItems.OrderByDescending(u => u.UserName).ToList()
                            : userItems.OrderBy(u => u.UserName).ToList(),

                        _ => userItems
                    };
                }


                return Results.Ok(userItems);
            }).WithSummary("Отримати користувачів із фільтрацією та сортуванням")
                .WithDescription("Підтримує параметри: id, name, sortBy (id/name), order (asc/desc).")
                .WithTags("Users"); ;

            //-------------------- GET /users/{id} — Отримати конкретного користувача --------------------
            app.MapGet("/users/{id:int}", (int id, string? name) =>
            {
                var userItems = JsonReaderAndWritter<UserClass>.ReadJsonFile(jsonPath);
                var user = userItems.FirstOrDefault(u => u.UserId == id);
                return user is not null ? Results.Ok(user) : Results.NotFound();
            });

            //--------------------------- POST /users — Додати нового користувача -------------------------
            app.MapPost("/users", (UserClass newUser) =>
            {
                var userItems = JsonReaderAndWritter<UserClass>.ReadJsonFile(jsonPath);

                // Автоінкремент UserId
                newUser.UserId = userItems.Count > 0 ? userItems.Max(u => u.UserId) + 1 : 1;

                if (newUser.UserName.Length <= 3 || newUser.UserName.Length > 100)
                    userItems.Add(newUser);
                JsonReaderAndWritter<UserClass>.WriteJsonFile(jsonPath, userItems);

                return Results.Created($"/users/{newUser.UserId}", newUser);
            });

            //--------------------------- PUT /users/{id} — Оновити користувача --------------------------
            app.MapPut("/users/{id:int}", (int id, UserClass updatedUser) =>
            {
                var userItems = JsonReaderAndWritter<UserClass>.ReadJsonFile(jsonPath);
                var user = userItems.FirstOrDefault(u => u.UserId == id);
                if (user is null) return Results.NotFound();

                user.UserName = updatedUser.UserName;

                JsonReaderAndWritter<UserClass>.WriteJsonFile(jsonPath, userItems);
                return Results.Ok(user);
            });

            //---------------------------- DELETE /users/{id} — Видалити користувача --------------------------
            app.MapDelete("/users/{id:int}", (int id) =>
            {
                var userItems = JsonReaderAndWritter<UserClass>.ReadJsonFile(jsonPath);
                var user = userItems.FirstOrDefault(u => u.UserId == id);
                if (user is null) return Results.NotFound();

                userItems.Remove(user);
                JsonReaderAndWritter<UserClass>.WriteJsonFile(jsonPath, userItems);

                return Results.NoContent();
            });
        }
    }
}
