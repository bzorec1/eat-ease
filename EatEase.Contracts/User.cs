namespace EatEase.Contracts;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
}