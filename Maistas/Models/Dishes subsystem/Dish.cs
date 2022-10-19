using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Dish : IEntityTypeConfiguration<Dish>
{
    public int Id { get; set; }

    public double Price { get; set; }

    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.Property(x => x.Price).IsRequired();
    }
}
