namespace EatEase.Contracts;

public record RegisterRequest
{
    public required string UserName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; } // TODO should encode

    public override string ToString()
    {
        return $"{nameof(UserName)}: {UserName}, {nameof(Email)}: {Email}";
    }
}