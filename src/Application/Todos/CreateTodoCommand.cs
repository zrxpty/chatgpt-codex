using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Todos;

public sealed record CreateTodoCommand(string Title, string? Description) : IRequest<Guid>;

public sealed class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
{
    private readonly ITodoRepository _repository;

    public CreateTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var item = new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _repository.AddAsync(item, cancellationToken);

        return item.Id;
    }
}
