using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;


public class OrderedDish : IEntityTypeConfiguration<OrderedDish>
{
    public int Id { get; set; }

    public int Quantity { get; set; }

	public int? DishId { get; set; }

	public Dish Dish { get; set; }

    public Order Order { get; set; }

	[NotMapped]
	public IList<SelectListItem> AvailableDishes { get; set; }

	public void Configure(EntityTypeBuilder<OrderedDish> builder)
    {

    }

	public async Task LoadAvailableDropdowns(FoodDbContext context)
	{

		var dishes = await context.Dishes.ToListAsync();

		AvailableDishes = dishes.Select(x =>
		{
			return new SelectListItem()
			{
				Value = Convert.ToString(x.Id),
				Text = x.Name,
			};
		}).ToList();
	}
}
