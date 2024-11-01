namespace EatEase.Contracts;

public record UserCreated
{
    public required Guid UserId { get; init; }
    
    public required string Username { get; init; }
    
    public required string Email { get; init; }
}