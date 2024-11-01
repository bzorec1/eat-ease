namespace EatEase.Contracts;

public record RegisterRequest
{
    public required string Username { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }

    public override string ToString()
    {
        return $"{nameof(Username)}: {Username}, {nameof(Email)}: {Email}";
    }
}