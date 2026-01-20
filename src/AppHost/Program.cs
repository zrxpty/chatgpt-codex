var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var todosDb = postgres.AddDatabase("todos");

builder.AddProject<Projects.Api>("api")
    .WithReference(todosDb);

builder.Build().Run();
