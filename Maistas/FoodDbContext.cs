using Maistas.Models;
using Maistas.Models.Dishes_subsystem;
using Microsoft.EntityFrameworkCore;

public class FoodDbContext : DbContext
{
    public FoodDbContext(DbContextOptions options) : base(options) { }

    public FoodDbContext() : base() { }

    public virtual DbSet<Dish> Dishes { get; set; } = null!;

    public virtual DbSet<Category> Categories { get; set; } = null!;

    public virtual DbSet<DishIngredient> DishIngredients { get; set; } = null!;

    public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;

    public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;

	public virtual DbSet<Order> Orders { get; set; } = null!;

    public virtual DbSet<OrderedDish> OrderedDishes { get; set; } = null!;

    public virtual DbSet<User> User { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodDbContext).Assembly);
    }

    
}