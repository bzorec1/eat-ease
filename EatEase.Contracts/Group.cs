namespace EatEase.Contracts;

public record Group(Guid Id, Guid GroupManagerId)
{
    public Guid Id { get; set; } = Id;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public List<string> LocationPreferences { get; set; } = null!;

    public Guid GroupManagerId { get; set; } = GroupManagerId;

    public List<Event> Events { get; set; } = new List<Event>();

    public List<User> Members { get; set; } = new List<User>();
}