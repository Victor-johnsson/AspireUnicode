var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder
    .AddProject<Projects.AspireUnicode_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.Build().Run();
