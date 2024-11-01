namespace EatEase.Contracts;

public record User
{
    public required Guid Id { get; init; }

    public required string Email { get; init; }

    public required string Username { get; init; }

    public string? Password { get; set; }
}