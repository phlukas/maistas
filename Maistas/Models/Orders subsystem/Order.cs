using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Duration { get; set; }
    public string Status { get; set; }
    public DateTime OrderTime { get; set; }
    public DateTime DeliveryTime { get; set; }
    public double TotalCost { get; set; }
    public double Distance { get; set; }
	public int RestaurantId { get; set; }
	public Restaurant Restaurant { get; set; }

    public List<OrderedDish> OrderedDish { get; set; }

    [NotMapped]
    public IList<SelectListItem> AvailableDishes { get; set; }

    [NotMapped]
    public IList<SelectListItem> AvailableRestaurants { get; set; }

    public Order()
    {

		this.OrderedDish = new List<OrderedDish>();
    }
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Date).HasMaxLength(255);
        builder.Property(x => x.TotalCost).HasMaxLength(12);
        builder.Property(x => x.OrderTime).HasMaxLength(255);
        builder.HasMany(x => x.OrderedDish).WithOne(x => x.Order);
    }

    public async Task LoadAvailableDropdowns(FoodDbContext context)
    {

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