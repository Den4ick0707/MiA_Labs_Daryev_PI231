using FluentValidation;
using LW_4_3_5_Daryev_PI231;
using LW_4_3_5_Daryev_PI231.Repositories;
using LW_4_3_5_Daryev_PI231.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});


builder.Services.AddScoped<IGamingAssetRepository, GamingAssetRepository>();

builder.Services.AddScoped<IGamingAssetService, GamingAssetService>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

