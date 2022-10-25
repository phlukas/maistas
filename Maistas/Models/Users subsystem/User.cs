using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maistas.Models;

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

    public void Configure(EntityTypeBuilder<User> builder)
    {
        
    }
}