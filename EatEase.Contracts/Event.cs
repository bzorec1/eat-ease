namespace EatEase.Contracts;

public class Event
{
    public Guid Id { get; set; }
    public EventType EventType { get; set; }
    public DateTime EventDateTime { get; set; }
    public string Location { get; set; } = null!;
    public EventStatus Status { get; set; }
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;
    public List<Expense> Expenses { get; set; } = new List<Expense>();
    public string Name { get; set; } = null!;
}