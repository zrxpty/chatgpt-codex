using Application.Abstractions;
using MediatR;

namespace Application.Todos;

public sealed record GetTodoQuery(Guid Id) : IRequest<TodoDto?>;

public sealed class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, TodoDto?>
{
    private readonly ITodoRepository _repository;

    public GetTodoQueryHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<TodoDto?> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetAsync(request.Id, cancellationToken);

        return item is null
            ? null
            : new TodoDto(item.Id, item.Title, item.Description, item.CreatedAt);
    }
}
