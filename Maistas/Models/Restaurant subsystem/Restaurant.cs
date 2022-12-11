using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Restaurant : IEntityTypeConfiguration<Restaurant>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PhoneNumber { get; set; }
    public string Website { get; set; }
    public string WorkTime { get; set; }
    public double MinimumOrderPrice { get; set; }
    public User? User { get; set; }
    public List<Dish> Dishes { get; set; }
    public int UserId { get; set; }
    
    [NotMapped]
    public IList<SelectListItem> AvailableUsers { get; set; }

    public Restaurant()
    {
        this.Dishes = new List<Dish>();
    }
    
    public async Task LoadAvailableDropdowns(FoodDbContext context)
    {
        var users = await context.User
            .Where(x => x.Role != "Restaurant")
            .ToListAsync();

        AvailableUsers = users.Select(x =>
        {
            return new SelectListItem()
            {
                Value = Convert.ToString(x.Id),
                Text = x.Username,
            };
        })
            .ToList();

    }
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(255);
        builder.Property(x => x.PhoneNumber).HasMaxLength(12);
        builder.Property(x => x.Website).HasMaxLength(255);
        builder.HasMany(x => x.Dishes).WithOne(x => x.Restaurant);
        builder.HasOne(x => x.User).WithOne(x => x.Restaurant);
    }
}