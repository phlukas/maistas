using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class User : IEntityTypeConfiguration<User>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string HelpQuestion { get; set; }
    public string CardInfo { get; set; }
    public string Address { get; set; }

    public Restaurant? Restaurant { get; set; }

    public User()
    {

    }

    public User(int id, string name, string surname, string email, string username, string password, string role, string helpQuestion, string cardInfo, string address)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Username = username;
        Password = password;
        Role = role;
        HelpQuestion = helpQuestion;
        CardInfo = cardInfo;
        Address = address;
    }

    public void Configure(EntityTypeBuilder<User> builder)
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
        builder.HasOne(x => x.Restaurant).WithOne(x => x.User).HasForeignKey<Restaurant>(x=>x.Id);
    }
}