using Domain.Entities;

namespace Application.Abstractions;

public interface ITodoRepository
{
    Task AddAsync(TodoItem item, CancellationToken cancellationToken);

    Task<TodoItem?> GetAsync(Guid id, CancellationToken cancellationToken);
}
