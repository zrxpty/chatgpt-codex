using Application.Abstractions;
using Domain.Entities;
using Marten;

namespace Infrastructure.Persistence;

public sealed class MartenTodoRepository : ITodoRepository
{
    private readonly IDocumentSession _session;

    public MartenTodoRepository(IDocumentSession session)
    {
        _session = session;
    }

    public async Task AddAsync(TodoItem item, CancellationToken cancellationToken)
    {
        _session.Store(item);
        await _session.SaveChangesAsync(cancellationToken);
    }

    public Task<TodoItem?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return _session.LoadAsync<TodoItem>(id, cancellationToken);
    }
}
