IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ProjectResource> backend = builder.AddAzureFunctionsProject<Projects.AzureFunctionBackend>("backend");

builder
    .AddProject<Projects.BlazorApp>("frontend")
    .WaitFor(backend);


builder.Build().Run();