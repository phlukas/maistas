using System.ComponentModel;
using Maistas.Models.Dishes_subsystem;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

public class Dish : IEntityTypeConfiguration<Dish>
{
    public int Id { get; set; }
    [DisplayName("Pavadinimas")]
    public string Name { get; set; }

    [DisplayName("Kaina")]
    public double Price { get; set; }
    
    [DisplayName("Likutis")]
    public int Remainder { get; set; }

    [DisplayName("Sukurta")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Kalorijos")]
    public int Calories { get; set; }

    [DisplayName("Aprašymas")]
    public string Description { get; set; }

    [DisplayName("Svoris")]
    public double WeightInGrams { get; set; }

    [DisplayName("Vegetariškas")]
    public bool Vegan { get; set; }

    public int CategoryId { get; set; }

    public int RestaurantId { get; set; }

    [DisplayName("Kategorija")]
    public Category Category { get; set; }

    [DisplayName("Restoranas")]
    public Restaurant Restaurant { get; set; }

    public List<DishIngredient> DishIngredients { get; set; }

    [NotMapped]
    public IList<SelectListItem> AvailableCategories { get; set; }

    [NotMapped]
    public IList<SelectListItem> AvailableRestaurants { get; set; }

    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(255);
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.HasOne(x => x.Category).WithMany(x => x.Dishes);
        builder.HasMany(x => x.DishIngredients).WithOne(x => x.Dish);
    }

    public async Task LoadAvailableDropdowns(FoodDbContext context)
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

        var restaurants = await context.Restaurants.ToListAsync();
        
        AvailableRestaurants = restaurants.Select(x =>
        {
            return new SelectListItem()
            {
                Value = Convert.ToString(x.Id),
                Text = x.Title,
            };
        }).ToList();
    }
}
