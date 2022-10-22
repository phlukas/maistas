using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maistas.Models.Dishes_subsystem;

public class DishIngredient : IEntityTypeConfiguration<DishIngredient>
{
    public int Id { get; set; }

    public Dish Dish { get; set; }

    public Ingredient Ingredient { get; set; }

    public void Configure(EntityTypeBuilder<DishIngredient> builder)
    {

    }
}
