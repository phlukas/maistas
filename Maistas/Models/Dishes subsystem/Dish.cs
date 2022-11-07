using Maistas.Models.Dishes_subsystem;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using Maistas.Models;

public class Dish : IEntityTypeConfiguration<Dish>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public int Remainder { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Calories { get; set; }

    public string Description { get; set; }

    public double WeightInGrams { get; set; }

    public bool Vegan { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
    
    public Restaurant Restaurant { get; set; }

    public List<DishIngredient> DishIngredients { get; set; }

    [NotMapped]
    public IList<SelectListItem> AvailableCategories { get; set; }

    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(255);
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.HasOne(x => x.Category).WithMany(x => x.Dishes);
        builder.HasMany(x => x.DishIngredients).WithOne(x => x.Dish);
    }

    public async Task LoadAvailableCategories(FoodDbContext context)
    {
        var categories = await context.Categories.ToListAsync();

        AvailableCategories = categories.Select(x =>
        {
            return new SelectListItem()
            {
                Value = Convert.ToString(x.Id),
                Text = x.Name,
            };
        }).ToList();
    }
}
