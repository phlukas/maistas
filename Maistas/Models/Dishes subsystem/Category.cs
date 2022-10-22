using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maistas.Models.Dishes_subsystem;

public class Category : IEntityTypeConfiguration<Category>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Dish> Dishes { get; set; }

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(255);
    }
}
