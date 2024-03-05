using EatEase.Domain.Events;
using EatEase.Domain.Users;

namespace EatEase.Domain.Groups;

public class Group
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public List<string> LocationPreferences { get; private set; } = null!;
    public Guid GroupManagerId { get; private set; }
    public List<Event> Events { get; private set; } = new List<Event>();
    public List<User> Members { get; private set; } = new List<User>();
}