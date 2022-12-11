
public class DishListView
{
    public double? PriceFrom { get; set; }
    public double? PriceTo { get; set; }
    public int? CaloriesFrom { get; set; }
    public int? CaloriesTo { get; set; }
    public double? WeightFrom { get; set; }
    public double? WeightTo { get; set; }
    public bool IsVegetarian { get; set; }

    public Restaurant Restaurant { get; set; }
    public List<Dish> Dishes { get; set; }
}