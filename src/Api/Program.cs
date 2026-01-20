using Application.Todos;
using Infrastructure;
using Marten;
using MediatR;
using Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(CreateTodoCommand).Assembly));

var connectionString = builder.Configuration.GetConnectionString("todos")
    ?? builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=localhost;Port=5432;Database=todos;Username=postgres;Password=postgres";

builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
    options.Schema.For<TodoItem>().Identity(x => x.Id);
});

builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/todos", async (CreateTodoCommand command, IMediator mediator) =>
{
    var id = await mediator.Send(command);
    return Results.Created($"/todos/{id}", new { Id = id });
});

app.MapGet("/todos/{id:guid}", async (Guid id, IMediator mediator) =>
{
    var todo = await mediator.Send(new GetTodoQuery(id));
    return todo is null ? Results.NotFound() : Results.Ok(todo);
});

app.Run();
