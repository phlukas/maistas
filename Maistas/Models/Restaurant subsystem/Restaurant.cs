using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Restaurant : User, IEntityTypeConfiguration<Restaurant>
{
   public string Title { get; set; }
    public string PhoneNumber { get; set; }
    public string Website { get; set; }
    public string WorkTime { get; set; }
    public double MinimumOrderPrice { get; set; }
    public List<Dish> Dishes { get; set; }

    public Restaurant()
    {
        this.Name = "";
        this.Surname = "";
        this.Username = "";
        this.Email = "";
        this.Password = "";
        this.Role = "";
        this.HelpQuestion = "";
        this.CardInfo = "";
        this.Dishes = new List<Dish>();
    }
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.Property(x=>x.Name).HasMaxLength(50);
        builder.Property(x=>x.Surname).HasMaxLength(50);
        builder.Property(x=>x.Email).HasMaxLength(255);
        builder.Property(x=>x.Username).HasMaxLength(25);
        builder.Property(x=>x.Password).HasMaxLength(255);
        builder.Property(x=>x.Role).HasMaxLength(50);
        builder.Property(x => x.HelpQuestion).HasMaxLength(500);
        builder.Property(x => x.CardInfo).HasMaxLength(100);
        builder.Property(x => x.Address).HasMaxLength(255);
        builder.Property(x => x.Title).HasMaxLength(255);
        builder.Property(x => x.PhoneNumber).HasMaxLength(12);
        builder.Property(x => x.Website).HasMaxLength(255);
        builder.HasMany(x => x.Dishes).WithOne(x => x.Restaurant);
    }
}