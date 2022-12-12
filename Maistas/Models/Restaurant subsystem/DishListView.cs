
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class DishListView
{
    public double? PriceFrom { get; set; }
    public double? PriceTo { get; set; }
    public int? CaloriesFrom { get; set; }
    public int? CaloriesTo { get; set; }
    public double? WeightFrom { get; set; }
    public double? WeightTo { get; set; }
    public bool IsVegetarian { get; set; }
    public int CategoryId { get; set; }
    public Restaurant Restaurant { get; set; }
    public List<Dish> Dishes { get; set; }
    public IList<SelectListItem> AvailableCategories { get; set; }
    public async Task LoadAvailableDropdowns(FoodDbContext context)
    {
        var categories = await context.Categories.ToListAsync();
        
        AvailableCategories = categories.Select(c =>
        {
            return new SelectListItem
            {
                Value = Convert.ToString(c.Id),
                Text = c.Name
            };
        })
            .ToList();
    }
    
}