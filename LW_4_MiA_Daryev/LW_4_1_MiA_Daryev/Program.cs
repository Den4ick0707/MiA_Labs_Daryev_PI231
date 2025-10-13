using LW_4_1_MiA_Daryev.Endpoints;
using Swashbuckle;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      
    app.UseSwaggerUI();    
}

app.MapUserEndpoints();
app.MapDeviceEndpoint();
app.MapLandlordEndpoint();
app.MapContractEndpoint();

app.Run();
