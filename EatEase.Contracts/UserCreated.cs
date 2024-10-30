namespace EatEase.Contracts;

public record UserCreated
{
    public Guid UserId { get; init; }
}