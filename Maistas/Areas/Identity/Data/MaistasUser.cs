using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


// Add profile data for application users by adding properties to the MaistasUser class
public class MaistasUser : IdentityUser<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CardInfo { get; set; }
    public string Address { get; set; }
    public Restaurant? Restaurant { get; set; }


    public void Configure(EntityTypeBuilder<MaistasUser> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Surname).HasMaxLength(50);
        builder.Property(x => x.CardInfo).HasMaxLength(100);
        builder.Property(x => x.Address).HasMaxLength(255);
        builder.HasOne(x => x.Restaurant).WithOne(x => x.User).HasForeignKey<Restaurant>(x => x.UserId);
    }
}
