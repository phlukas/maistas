using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maistas.Models.Dishes_subsystem;

public class Ingredient : IEntityTypeConfiguration<Ingredient>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<DishIngredient> DishIngredients { get; set; }

    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(255);
        builder.HasMany(x => x.DishIngredients).WithOne(x => x.Ingredient);
    }
}
