using EatEase.Domain.Events;
using EatEase.Domain.Users;

namespace EatEase.Domain.Expenses;

public class Expense
{
    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }
    public ExpenseCategory Category { get; private set; }
    public Guid PaidByUserId { get; private set; }
    public User PaidBy { get; private set; }
    public Guid EventId { get; private set; }
    public Event Event { get; private set; }
}
