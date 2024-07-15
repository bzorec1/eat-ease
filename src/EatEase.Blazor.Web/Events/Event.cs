using System.Text.RegularExpressions;
using EatEase.Domain.Expenses;

namespace EatEase.Domain.Events;

public class Event
{
    public Guid Id { get; private set; }
    public EventType EventType { get; private set; }
    public DateTime EventDateTime { get; private set; }
    public string Location { get; private set; } = null!;
    public EventStatus Status { get; private set; }
    public Guid GroupId { get; private set; }
    public Group Group { get; private set; } = null!;
    public List<Expense> Expenses { get; private set; } = new List<Expense>();
}
