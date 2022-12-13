using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;

public class Category : IEntityTypeConfiguration<Category>
{
    public int Id { get; set; }

    [DisplayName("Pavadinimas")]
    public string Name { get; set; }

    public List<Dish> Dishes { get; set; }

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(255);
    }
}
