namespace Application.Todos;

public sealed record TodoDto(
    Guid Id,
    string Title,
    string? Description,
    DateTimeOffset CreatedAt);
