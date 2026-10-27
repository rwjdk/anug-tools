IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

// Run the npm tools as ordinary processes; no container runtime is required.
string command = OperatingSystem.IsWindows() ? "cmd.exe" : "/bin/sh";
string commandSwitch = OperatingSystem.IsWindows() ? "/c" : "-c";
var storage = builder.AddExecutable("storage", command, "..",
    commandSwitch, "azurite --silent --location .azurite")
    .WithHttpEndpoint(port: 10000, targetPort: 10000, name: "blob", isProxied: false)
    .WithHttpEndpoint(port: 10001, targetPort: 10001, name: "queue", isProxied: false)
    .WithHttpEndpoint(port: 10002, targetPort: 10002, name: "table", isProxied: false);

#if DEBUG
const string configuration = "Debug";
#else
const string configuration = "Release";
#endif
var backend = builder.AddExecutable("backend", command, "../AzureFunctionBackend",
    commandSwitch,
    $"func start --script-root bin/{configuration}/net10.0 --port 7083 --cors https://localhost:7112,http://localhost:5057")
    .WithEnvironment("FUNCTIONS_WORKER_RUNTIME", "dotnet-isolated")
    .WithEnvironment("AzureWebJobsStorage", "UseDevelopmentStorage=true")
    .WithHttpEndpoint(port: 7083, targetPort: 7083, isProxied: false)
    .WaitFor(storage);

builder.AddProject<Projects.BlazorApp>("frontend", launchProfileName: "https")
    .WithEndpoint("https", endpoint => endpoint.Port = 7112)
    .WithEndpoint("http", endpoint => endpoint.Port = 5057)
    .WaitFor(backend);

builder.Build().Run();
