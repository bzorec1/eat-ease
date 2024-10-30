using EatEase.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EatEase.Messaging;

public sealed class UserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await _context.Users.SingleOrDefaultAsync(user => user.Id == userId, cancellationToken);

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        await _context.Users.SingleOrDefaultAsync(user => user.Email == email, cancellationToken);

    public async Task<User?> GetUserByUserNameAsync(string username, CancellationToken cancellationToken = default) =>
        await _context.Users.SingleOrDefaultAsync(user => user.Username == username, cancellationToken);
}