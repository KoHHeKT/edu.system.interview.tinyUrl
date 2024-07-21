using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<System_Interview_TinyUrl_ByHash>("tinyUrl-byHash")
    .WithExternalHttpEndpoints();

builder.Build().Run();
