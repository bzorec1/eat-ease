namespace EatEase.Contracts;

public record LoginRequest
{
    public required string Identifier { get; init; }

    public required string Password { get; init; }

    public override string ToString()
    {
        return $"{nameof(Identifier)}: {Identifier}";
    }
}