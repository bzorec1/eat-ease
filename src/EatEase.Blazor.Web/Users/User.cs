namespace EatEase.Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Surname { get; private set; } = null!;
}