using EatEase.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EatEase.Messaging;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }


    public DbSet<User> Users { get; set; }
}