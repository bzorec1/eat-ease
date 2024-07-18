using EatEase.Blazor.Web.Events;
using EatEase.Blazor.Web.Users;

namespace EatEase.Blazor.Web.Expenses;

public class Expense
{
    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; } = null!;
    public ExpenseCategory Category { get; private set; }
    public Guid PaidByUserId { get; private set; }
    public User PaidBy { get; private set; } = null!;
    public Guid EventId { get; private set; }
    public Event Event { get; private set; } = null!;
}
