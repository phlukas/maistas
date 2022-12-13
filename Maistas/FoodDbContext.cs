using Maistas.Models.Dishes_subsystem;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Maistas.Models.Users_subsystem;

public class FoodDbContext : IdentityDbContext<MaistasUser,IdentityRole<int>, int>
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

    public virtual DbSet<MaistasUser> MaistasUser { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Maistas.Models.Users_subsystem.ProjectRole> ProjectRole { get; set; }

    
}