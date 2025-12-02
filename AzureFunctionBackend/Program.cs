using AzureFunctionBackend;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedModels.Services;

var builder = FunctionsApplication.CreateBuilder(args);
builder.Services.AddScoped<ParticipationWinnerSelector>();
builder.Services.AddScoped<MeetupEventService>();

builder.ConfigureMcpTool(Constants.ToolName);

builder.Build().Run();