using AdmCC.Api.Configurations;
using AdmCC.InfraData.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AdmCCConnection")
    ?? throw new InvalidOperationException("A connection string 'AdmCCConnection' nao foi configurada.");

builder.Services.AddApiPresentation();
builder.Services.AddAdmCCInfrastructure(connectionString);

var app = builder.Build();

app.UseApiDocumentation();

app.UseAuthorization();

app.MapControllers();

app.Run();
