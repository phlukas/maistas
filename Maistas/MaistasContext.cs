using Microsoft.EntityFrameworkCore;

public class MaistasContext : DbContext
{
    public MaistasContext(DbContextOptions options) : base(options) { }

    public MaistasContext() : base() { }

    public virtual DbSet<Dish> Dishes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MaistasContext).Assembly);
    }
}