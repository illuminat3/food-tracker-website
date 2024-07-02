using FoodTracker.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.DbContext;

public class FoodTrackerContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<User> Users { get; set; }

    public FoodTrackerContext(DbContextOptions<FoodTrackerContext> options) : base(options)
    {
    }
}