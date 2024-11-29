using EatEase.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EatEase.BlazorApp.Infrastructure;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }


    public DbSet<User> Users { get; set; }
}